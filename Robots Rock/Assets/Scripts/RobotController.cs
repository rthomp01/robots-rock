using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public float HorizontalMove { get; set; }

    //set in inspector
    public Animator animator;
    public ControllerConfig config;

    //private
    private Quaternion goalRotation;
    private float inputVelocity;
    private float xInput;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        goalRotation = transform.rotation;
    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        HorizontalMove = Mathf.SmoothDamp(HorizontalMove, xInput, ref inputVelocity, config.inputDampening);
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = new Vector3(HorizontalMove * config.walkSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
        Debug.Log(Rigidbody.velocity);
        UpdateRotation();
    }

    void UpdateRotation()
    {
        if (xInput != 0.0f)
        {
            goalRotation = xInput > 0.0f ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(-Vector3.right);
        }
        Rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, config.turnRate * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("WalkSpeed", Mathf.Abs(HorizontalMove));
    }
}
