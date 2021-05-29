// Some stupid rigidbody based movement by Dani

// using code by other people is not cheating it's working efficently (Robbie)
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Assingables
    public Transform playerCam;
    public Transform orientation;
    public GameObject head;
    Inputs inputs;
    public LayerMask runable;
    public Animator ani;

    //public AudioSource PlayerSource;

    //Other
    private Rigidbody rb;
    public float Grav;
    public bool playScript = true;

    //Rotation and look
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    //Movement
    public float moveSpeed = 4500;
    public float wallSpeed;
    public float maxSpeed = 20;
    public float maxWallSpeed;
    public bool grounded;
    public bool Grounded2;
    public bool wasGrounded;
    float groundedTimer;
    float jumpTimer;

    float diveAmount;
    public LayerMask whatIsGround;
    public bool isWallRuning;
    public float wallJumpForce;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;
    public Collider col;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    Vector3 coliderNormal;

    //Input
    float x, y;
    bool jumping, sprinting, crouching;

    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    bool hasDiven = false;
    private float startHight;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new Inputs();
        inputs.Player.Enable();

    }

    void Start()
    {
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Crouching
        inputs.Player.crouch.started += _ => StartCrouch();
        inputs.Player.crouch.canceled += _ => StopCrouch();
        inputs.Player.ESC.started += _ => UnLock();
        inputs.Player.Jump.started += _ => wallJump();
        Grav = Physics.gravity.y;
    }

    void UnLock()
    {


        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        Cursor.visible = !Cursor.visible;
    }

    private void FixedUpdate()
    {
        if (!playScript) return;
        Movement();
    }

    private void Update()
    {
        if (!playScript) return;
        MyInput();
        Look();
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }
        if (grounded)
        {
            groundedTimer = 0.2f;
        }
        if (!readyToJump)
        {
            groundedTimer = 0;
            jumpTimer = 0;
        }
        //wind();




        //&& !Physics.CapsuleCast(head.transform.position, head.transform.position, 0.1f, orientation.forward.normalized, 0.8f, 9)

        if (isWallRuning )
        {


            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Vector3 surfaceParallel = new Vector3((orientation.forward.x), (orientation.forward.y), (orientation.forward.z)) - new Vector3(coliderNormal.x, coliderNormal.y, coliderNormal.z) * Vector3.Dot(orientation.forward, coliderNormal);



            //Debug.Log(surfaceParallel + "  surface parrele");



            rb.AddForce(20 * coliderNormal * -1 * Time.deltaTime);

            if (rb.velocity.magnitude < maxWallSpeed)
            {

                rb.AddForce(surfaceParallel * Time.deltaTime * wallSpeed);
            }
        }
        else
        {
            Physics.gravity = new Vector3(0, Grav, 0);
        }

        if (!isWallRuning)
        {
            Physics.gravity = new Vector3(0, Grav, 0);
        }

    }
    private void LateUpdate()
    {
        if (!playScript) return;
        if (Grounded2 && !wasGrounded)
        {
            //put head down a bit

            ani.SetTrigger("Landing");
        }

        wasGrounded = Grounded2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(head.transform.position + this.transform.forward * 0.3f, this.transform.forward * 0.8f + head.transform.position);
    }
    /// <summary>
    /// Find user input. Should put this in its own class but im lazy
    /// </summary>
    private void MyInput()
    {
        if (!playScript) return;
        y = inputs.Player.Xaxis.ReadValue<float>();
        x = inputs.Player.Yaxis.ReadValue<float>();

        if (inputs.Player.Jump.ReadValue<float>() > 0)
        {
            jumping = true;
            jumpTimer = 0.2f;
        }
        else
        {
            jumping = false;
        }

        if (inputs.Player.crouch.ReadValue<float>() > 0)
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
    }

    private void StartCrouch()
    {
        crouching = true;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        crouching = false;
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        if (!playScript) return;
        //Extra gravity
        if (!grounded)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 10);
        }
        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumpTimer > 0) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        //if (grounded && readyToJump)
        //{
        //    rb.AddForce(Vector3.down * Time.deltaTime * 3000);
        //    return;
        //}

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (!crouching)
        {
            if (x > 0 && xMag > maxSpeed) x = 0;
            if (x < 0 && xMag < -maxSpeed) x = 0;
            if (y > 0 && yMag > maxSpeed) y = 0;
            if (y < 0 && yMag < -maxSpeed) y = 0;
        }
        if (crouching)
        {
            if (x > 0 && xMag > maxSpeed * 0.5) x = 0;
            if (x < 0 && xMag < -maxSpeed * 0.5) x = 0;
            if (y > 0 && yMag > maxSpeed * 0.5) y = 0;
            if (y < 0 && yMag < -maxSpeed * 0.5) y = 0;
        }
        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.75f;
            multiplierV = 0.75f;
        }

        // Movement while sliding
        if (grounded && crouching) multiplierV = 0.5f;
        if (grounded && crouching) multiplier = 0.5f;
        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if (!playScript) return;
        if (groundedTimer > 0 && readyToJump)
        {
            readyToJump = false;
            groundedTimer = 0;
            jumpTimer = 0;
            //Add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    void wallJump()
    {

        if (!playScript) return;

        if (isWallRuning && readyToJump)
        {
            Vector3 surfaceParallel = new Vector3(Mathf.Abs(orientation.forward.x), Mathf.Abs(orientation.forward.y), Mathf.Abs(orientation.forward.z)) - new Vector3(coliderNormal.x, coliderNormal.y, coliderNormal.z) * Vector3.Dot(orientation.forward, coliderNormal);
            //if(Vector3.forward.x > 0.1f || Vector3.forward.z > 0.1f)
            //        {
            //            surfaceParallel *= -1;
            //        }

            surfaceParallel = new Vector3(surfaceParallel.x * Mathf.RoundToInt(orientation.forward.x), surfaceParallel.y * Mathf.RoundToInt(orientation.forward.y), surfaceParallel.z * Mathf.RoundToInt(orientation.forward.z));

            this.transform.position += coliderNormal * 2;

            rb.AddForce(new Vector3(coliderNormal.x, 0, coliderNormal.z) * wallJumpForce * 0.5f);
            rb.AddForce(this.transform.up * wallJumpForce * 0.4f);

            rb.AddForce(orientation.forward * wallJumpForce * 0.01f);

            rb.AddForce(surfaceParallel * wallJumpForce * 0.2f); Debug.Log("wall jump");
            isWallRuning = false;

            inputs.Player.Xaxis.Disable();
            inputs.Player.Yaxis.Disable();
            Invoke(nameof(ResetWallJump), 0.4f);
        }
    }
    private void ResetWallJump()
    {
        inputs.Player.Xaxis.Enable();
        inputs.Player.Yaxis.Enable();
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

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!playScript) return;
        if (!grounded || jumping) return;

        //Slow down sliding
        //if (crouching)
        //{
        //    rb.AddForce(Time.deltaTime * -rb.velocity.normalized / 2 * slideCounterMovement + orientation.transform.forward * 20);
        //}

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    /// <summary>
    /// Handle ground detection
    /// </summary>
    ///

    private void OnCollisionEnter(Collision other)
    {
        if (!playScript) return;
        //&& !Physics.CapsuleCast(head.transform.position, head.transform.position, 0.1f, orientation.forward.normalized, 0.8f, 9)
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;

        if (runable != (runable | (1 << layer)) && whatIsGround != (whatIsGround | (1 << layer)))
        {
            Vector3 moveto = other.collider.ClosestPoint(transform.position);
            this.transform.position += new Vector3(moveto.x * other.contacts[0].normal.normalized.y * 0.15f, 0, moveto.z * other.contacts[0].normal.normalized.y * 0.15f);
            isWallRuning = true;
            Physics.gravity = Vector3.zero;
            coliderNormal = other.contacts[0].normal.normalized;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            col = other.collider;
        }
        else
        {
            Physics.gravity = new Vector3(0, Grav, 0);
            isWallRuning = false;
        }

        if (9 == (9 | (1 << layer)))
        {
            Physics.gravity = new Vector3(0, Grav, 0);
            isWallRuning = false;
        }

    }


    private void OnCollisionStay(Collision other)
    {
        if (!playScript) return;
        //&& !Physics.CapsuleCast(head.transform.position, head.transform.position, 0.1f, orientation.forward.normalized, 0.8f, 9)
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;

        if (runable != (runable | (1 << layer)) && whatIsGround != (whatIsGround | (1 << layer)) )
        {
        //    Vector3 moveto = other.collider.ClosestPoint(transform.position);
        //    this.transform.position += new Vector3(moveto.x * other.contacts[0].normal.normalized.y * 0.15f, 0, moveto.z * other.contacts[0].normal.normalized.y * 0.15f);
        //    isWallRuning = true;
        //    Physics.gravity = Vector3.zero;
        //    coliderNormal = other.contacts[0].normal.normalized;
        //    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //    col = other.collider;
        }
        else
        {
            Physics.gravity = new Vector3(0, Grav, 0);
            isWallRuning = false;
        }

        if(9 == (9 | (1<< layer)))
        {
            Physics.gravity = new Vector3(0, Grav, 0);
            isWallRuning = false;
        }
            

        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!playScript) return;
        Grounded2 = true;

    }

    private void StopGrounded()
    {
        if (!playScript) return;
        grounded = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!playScript) return;
        isWallRuning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playScript) return;
    }
    private void OnTriggerExit(Collider other) {

        if (!playScript) return;
        Grounded2 = false;
        
    }

    //void wind()
    //{
    //    if (grounded) {PlayerSource3.Pause();
    //    return; }
    //    PlayerSource3.UnPause();
    //    if (rb.velocity.x + rb.velocity.z + rb.velocity.y > 5)
    //    {
    //        PlayerSource3.volume = (rb.velocity.x + rb.velocity.z + rb.velocity.y) / 15;
    //    } else if (rb.velocity.x + rb.velocity.z + rb.velocity.y < -5)
    //    {
    //        PlayerSource3.volume = -(rb.velocity.x + rb.velocity.z + rb.velocity.y) / 15;
    //    }
    //}

}