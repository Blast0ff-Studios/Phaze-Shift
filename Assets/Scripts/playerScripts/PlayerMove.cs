using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public bool playScript = true;

    [Header("Assignables")]
    public Transform playerCam;
    public Transform orientation;
    public GameObject head;

    [Space]
    [Header("Camera")]
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    [Space]
    [Header("Jump")]
    public float JumpForce;
    float JumpTime;
    public float JumpTimerLength;
    public float JumpReleaseMultiplier;
    public Vector2[] GravityModifier;
    

    //public Vector3 GroundedCheckPosition;
    [Space]
    [Header("GroundCheck")]
    public LayerMask floor;
    public float GroundCheckY;
    public float GroundedCheckLength;
    public float SlopeCheckLength;
    [HideInInspector] public float GroundedTime;
    public float GroundedTimerLength;

    [Space]
    [Header("Walk")]
    public float WalkSpeed;
    public float ForwardBackMultiplier;
    public float LeftRightMultiplier;
    public float MaxSpeed;
    public float FrictionMultiplier;
    public float StrafeSpeedY;
    public float StrafeSpeedX;
    public float StrafeVelocityMultiplyer;
    bool canMove;


    [Space]
    [Header("Dash")]
    public float DashSpeed;
    public float DashSpeedUpFlat;
    public float DashCooldown;
    [HideInInspector] public float CanDash;
    bool DashCharging;

    [Space]
    [Header("Glide")]
    public float GlideChargeMultiplier;
    [HideInInspector] public float GlideLength;
    public float GlideSpeed;
    public float GlideLengthMax;
    bool GlideCharge;

    [Space]
    [Header("WallRun")]
    public LayerMask WhatIsWall;
    public float WallRunSpeed, MaxWallSpeed, WallJumpForce;
    bool isWallRight, isWallLeft;
    public float MaxWallCameraTilt, WallCameraTilt;
    public float UpSideJumpRatio;
    bool isWallRunning;
    public float WallRayLength;
    public float WallTractionForce;


    //UnIteractables
    [HideInInspector] public float velocity;

    bool JumpFrame;
    bool MoveFrame;

    Inputs inputs;
    Rigidbody rb;
    float gravity;
    float V;

    bool canGround = true;
    Ray GroundRay;
    RaycastHit GroundRayReturn;

    [HideInInspector] public float y;
    [HideInInspector] public float x;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new Inputs();
        inputs.Player.Enable();


    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Crouching
        inputs.Player.crouch.started += _ => StartDash();

        inputs.Player.Jump.started += _ => JumpInput();
        inputs.Player.Jump.canceled += _ => JumpCancel();
        gravity = Physics.gravity.y;
    }

    void LoggingTest()
    {
        Debug.Log("Performed");
    }

    // Update is called once per frame
    void Update()
    {

        VaribleSetting();
        Debug.Log(inputs.Player.crouch.phase);

        if (!playScript)
        {
            inputs.Player.Disable();
            return;
        }
        else
        {
            inputs.Player.Enable();
        }

        IsGrounded();



        Gravity();

        CallVoids();
    }

    void CallVoids()
    {
        //Debug.Log("G " + gravity + " CG " + Physics.gravity.y + " " + JumpTime + " " + GroundedTime);


        Friction();

        Jump();

        Move();

        Look();

        CheckForWall();
        WallUpdate();
        WallRunInput();

        Glide();

        Timers();
    }

    void Gravity()
    {


        if (GravityModifier[GravityModifier.Length - 1].x <= rb.velocity.y)
        {
            Physics.gravity = new Vector3(0, gravity * GravityModifier[GravityModifier.Length - 1].y, 0);
            Debug.Log(GravityModifier.Length - 1);
        }

        for (int i = 0; i < GravityModifier.Length - 1; i++) 
        {
            if (GravityModifier[i].x <= rb.velocity.y && GravityModifier[i + 1].x > rb.velocity.y)
            {
                Physics.gravity = new Vector3(0, gravity * GravityModifier[i].y, 0);

            }
        }




        if (GroundedTime > 0)
        {
            Physics.gravity = new Vector3(0, gravity);
        }
    }

    void VaribleSetting()
    {
        if (JumpFrame == true)
        {
            V = rb.velocity.y;
        }
        JumpFrame = false;

        if (!MoveFrame)
        {
            velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y).magnitude;
        }
        MoveFrame = false;

        y = inputs.Player.Xaxis.ReadValue<float>() * ForwardBackMultiplier;
        x = inputs.Player.Yaxis.ReadValue<float>() * LeftRightMultiplier;
    }

    void Timers()
    {
        //Jump
        JumpTime -= Time.deltaTime;

        //Ground
        GroundedTime -= Time.deltaTime;

        //Dash
        {
            if (GroundedTime > 0 || DashCharging || isWallRunning)
            {
                CanDash -= Time.deltaTime;
                DashCharging = true;
            }
        }

        //Gliding
        {
            if (((GroundedTime > 0 || isWallRunning)|| GlideCharge) && GlideLength <= GlideLengthMax)
            {
                GlideLength += GlideChargeMultiplier * Time.deltaTime;
                GlideCharge = true;
            }
            if (GlideLength > GlideLengthMax)
            {
                GlideLength = GlideLengthMax;
            }
        }



    }

    void Move()
    {
        if (canMove)
        {
            if (GroundedTime >= 0.18)
            {
                if (GroundRayReturn.normal != Vector3.up)
                {
                    rb.AddForce(Vector3.ProjectOnPlane(orientation.forward * y * WalkSpeed * Time.deltaTime +
                            orientation.right * x * WalkSpeed * Time.deltaTime,
                            GroundRayReturn.normal));
                }
                else
                {
                    rb.AddForce(orientation.forward * y * WalkSpeed * Time.deltaTime + orientation.right * x * WalkSpeed * Time.deltaTime);
                }

                if (rb.velocity.magnitude > MaxSpeed)
                {
                    rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * MaxSpeed, 35 * Time.deltaTime);
                    rb.velocity = new Vector3(rb.velocity.x, V, rb.velocity.z);
                }
            }
            else
            {

                Vector3 NewV = (orientation.forward * y * Time.deltaTime)
                               + (orientation.right * x * Time.deltaTime);

                if (rb.velocity.magnitude < MaxSpeed)
                {
                    rb.AddForce(NewV.normalized * StrafeSpeedY * Time.deltaTime);

                }
                else
                if (NewV.magnitude > 0 && NewV.normalized != new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized)
                {
                    rb.velocity -= new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized * StrafeVelocityMultiplyer * Time.deltaTime;
                    rb.velocity += NewV.normalized * StrafeVelocityMultiplyer * Time.deltaTime;
                }
            }
        }
    }


    void JumpInput()
    {
        JumpTime = JumpTimerLength;
    }

    void Jump()
    {
        if (JumpTime > 0 && GroundedTime > 0)
        {
            Physics.gravity = new Vector3(0, gravity, 0);

            rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);

            JumpTime = 0;
            GroundedTime = 0;
            canGround = false;
            Invoke("GroundReset", 0.1f);

            V = JumpForce;
            JumpFrame = true;
        }

        if (isWallRunning && JumpTime > 0)
        {
            Physics.gravity = new Vector3(0, gravity, 0);
            JumpTime = 0;


            if (isWallRight)
            {
                rb.AddForce(-orientation.right * WallJumpForce * (1 - UpSideJumpRatio) + orientation.up * WallJumpForce * UpSideJumpRatio);
            }
            else if (isWallLeft)
            {
                rb.AddForce(orientation.right * WallJumpForce * (1 - UpSideJumpRatio) + orientation.up * WallJumpForce * UpSideJumpRatio);
            }

            rb.AddForce(orientation.forward * WallJumpForce * 0.2f);

        }

        JumpFrame = true;

    }

    void GroundReset()
    {
        canGround = true;
    }

    void JumpCancel()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * JumpReleaseMultiplier, rb.velocity.z);
        }
    }



    void WallUpdate()
    {
        //While Wallrunning
        //Tilts camera in .5 second
        if (Mathf.Abs(WallCameraTilt) < MaxWallCameraTilt && isWallRunning && isWallRight)
            WallCameraTilt += Time.deltaTime * MaxWallCameraTilt * 2;
        if (Mathf.Abs(WallCameraTilt) < MaxWallCameraTilt && isWallRunning && isWallLeft)
            WallCameraTilt -= Time.deltaTime * MaxWallCameraTilt * 2;

        //Tilts camera back again
        if (WallCameraTilt > 0 && !isWallRight && !isWallLeft)
            WallCameraTilt -= Time.deltaTime * MaxWallCameraTilt * 2;
        if (WallCameraTilt < 0 && !isWallRight && !isWallLeft)
            WallCameraTilt += Time.deltaTime * MaxWallCameraTilt * 2;

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, WallCameraTilt);

    }
    void WallRunInput()
    {
        if (x > 0f && isWallRight) { StartWallRun(); }
        if (x < 0f && isWallLeft) { StartWallRun(); }
    }
    void CheckForWall()
    {


        isWallRight = Physics.Raycast(transform.position, orientation.right, WallRayLength, WhatIsWall);
        isWallLeft = Physics.Raycast(transform.position, orientation.right * -1f, WallRayLength, WhatIsWall);

        if (GroundedTime > 0)
        {

            isWallRight = false;
            isWallLeft = false;

        }

        if (!isWallRight && !isWallLeft)
        {
            EndWallRun();
        }

    }
    void StartWallRun()
    {
        rb.useGravity = false;
        if (rb.velocity.y < 0 && rb.velocity.y > -5)
        {
            rb.velocity = rb.velocity + new Vector3(0, rb.velocity.y * 0.5f, 0);
        }
        else if (rb.velocity.y < -5)
        {
            rb.velocity = rb.velocity + new Vector3(0, -rb.velocity.y * 0.2f, 0);
        }

        isWallRunning = true;
        canMove = false;

        if (rb.velocity.magnitude <= MaxWallSpeed)
        {
            rb.AddForce(orientation.forward * WallRunSpeed * Time.deltaTime);


        }
        if (isWallRight)
        {
            rb.AddForce(orientation.right * WallTractionForce * rb.velocity.magnitude * Time.deltaTime);
        }
        else
        {
            rb.AddForce(-orientation.right * WallTractionForce * rb.velocity.magnitude * Time.deltaTime);
        }
    }
    void EndWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
        Invoke("MoveAgain", 0.3f);
    }


    void MoveAgain()
    {
        canMove = true;
    }

    void StartDash()
    {
        if (CanDash <= 0 && !isWallRunning)
        {
            if (y <= 0)
            {
                Physics.gravity = new Vector3(0, gravity, 0);

                if (rb.velocity.y < 5)
                {
                    rb.velocity = new Vector3(rb.velocity.x, DashSpeedUpFlat, rb.velocity.z);
                }
                else
                {
                    rb.velocity += new Vector3(0, DashSpeedUpFlat * 0.5f, 0);
                }

                GroundedTime = 0;
                canGround = false;
                Invoke("GroundReset", 0.1f);

                CanDash = DashCooldown;
                DashCharging = false;

            }
            else
            {
                rb.AddForce(orientation.forward * DashSpeed * 10);
                CanDash = DashCooldown;
                DashCharging = false;


            }

        }
    }

    void Glide()
    {
        if (inputs.Player.Jump.ReadValue<float>() == 1 && rb.velocity.y < -1 * GlideSpeed && GlideLength >= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -1 * GlideSpeed, rb.velocity.z);
            GlideLength -= Time.deltaTime;
            GlideCharge = false;

            V = -GlideSpeed;
        }
    }

    bool IsGrounded()
    {
        GroundRay.direction = -this.transform.up;
        GroundRay.origin = this.transform.position;
        Physics.Raycast(GroundRay, out GroundRayReturn, GroundedCheckLength, floor, QueryTriggerInteraction.Ignore);

        if (Physics.Raycast(GroundRay, out GroundRayReturn, GroundedCheckLength, floor, QueryTriggerInteraction.Ignore) && canGround)
        {
            // + this.transform.position
            GroundedTime = GroundedTimerLength;


        }
        return Physics.Raycast(this.transform.position, -this.transform.up, GroundedCheckLength, floor, QueryTriggerInteraction.Ignore);


    }

    private float desiredX;
    private void Look()
    {
        if (!playScript) return;
        float mouseX = inputs.Player.mousePos.ReadValue<Vector2>().x * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = inputs.Player.mousePos.ReadValue<Vector2>().y * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    void Friction()
    {
        if (GroundedTime > 0.17f && x == 0 && y == 0)
        {
            rb.velocity *= FrictionMultiplier;
        }
    }
}