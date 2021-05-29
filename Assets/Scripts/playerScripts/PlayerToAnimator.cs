using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;


public class PlayerToAnimator : MonoBehaviour
{
    public Slider DashSlider;
    public Slider GlideSlider;
    public PlayerMove PM;
    public Animator Ani;
    public Camera Camera;

    bool HasDashed = false;
    bool HasGrounded = false;


    public float baseFieldOfVeiw;
    public float fieldOfVeiwOffset;
    public float undampedOffset;

    public float ViewDamping;
    public float speedOffset;
    public float speedMultiplier;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera.fieldOfView = baseFieldOfVeiw;

        DashSlider.maxValue = PM.DashCooldown;
        GlideSlider.maxValue = PM.GlideLengthMax;
    }

    // Update is called once per frame
    void Update()
    {
        GlideSlider.value = PM.GlideLength;
        DashSlider.value = PM.CanDash;
        if (PM.CanDash == PM.DashCooldown && !HasDashed && PM.y <= 0)
        {
            Ani.SetTrigger("Dash");
            HasDashed = true;
        }
        if (PM.CanDash != PM.DashCooldown)
        {
            HasDashed = false;
        }


        Ani.SetFloat("Speed", rb.velocity.magnitude);


        if (PM.GroundedTime > 0)
        {
            Ani.SetBool("Grounded", true);
        }
        else
        {
            Ani.SetBool("Grounded", false);
        }
        if (PM.GroundedTime >= PM.GroundedTimerLength - 0.1f && !HasGrounded && rb.velocity.y <= 0)
        {
            Ani.SetTrigger("Landing");
            HasGrounded = true;
        }
        if (PM.GroundedTime <= PM.GroundedTimerLength - 0.1f)
        {
            HasGrounded = false;
            Ani.ResetTrigger("Landing");
        }


        Camera.fieldOfView = undampedOffset + Mathf.Lerp(Camera.fieldOfView, baseFieldOfVeiw + (fieldOfVeiwOffset + Mathf.Clamp(((rb.velocity.magnitude - speedOffset) * speedMultiplier), 0, 20) * 0.5f), Time.deltaTime * ViewDamping);
    }

    void World()
    {
        Ani.speed = 1 / 0.000001f;
        Ani.SetTrigger("worldChange");
    }
    void UnWorld()
    {
        Ani.speed = 1;

    }
}