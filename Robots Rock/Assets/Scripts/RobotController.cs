using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public Vector2 InputVector { get; private set; }

    //set in inspector
    public Animator animator;
    public float moveSpeed = 3.0f;
    public float turnRate = 45.0f;
    public float locomotionDampening = 0.1f;

    //private
    private Quaternion goalRotation;
    private float inputVelocity;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        goalRotation = transform.rotation;

        if(animator == null)
        {
            Debug.LogFormat("Animator reference mission on {0}!", transform.name);
            this.enabled = false;
        }
    }

    private void Update()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = new Vector3(InputVector.x * moveSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
        UpdateRotation();
    }

    void UpdateRotation()
    {
        if(InputVector.x != 0.0f)
        {
            goalRotation = InputVector.x > 0.0f ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(-Vector3.right);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnRate * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("WalkSpeed", Mathf.SmoothDamp(animator.GetFloat("WalkSpeed"), Mathf.Abs(InputVector.x),
            ref inputVelocity, locomotionDampening));
    }
}
