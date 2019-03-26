using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : BaseController
{
    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput PlayerInput { get; set; }

    //set in inspector
    public Animator animator;
    public ControllerConfig config;

    //private
    private Quaternion goalRotation;


    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponent<Rigidbody>();
        goalRotation = transform.rotation;

        PlayerInput = new PlayerInput();

        //default state
        StateMachine.ChangeState(new RobotStateGrounded(this));
    }

    private void Update()
    {
        GetInput();
        StateMachine.Execute();
    }

    private void FixedUpdate()
    {
        StateMachine.ExecuteFixed();
        UpdateRotation();
    }

    void GetInput()
    {
        PlayerInput.HorizontalMove = Input.GetAxisRaw("Horizontal");
        PlayerInput.JumpPressed = Input.GetButtonDown("Jump");
        PlayerInput.AttackPressed = Input.GetButtonDown("Attack");
    }

    public void UpdateRotation()
    {
        if (PlayerInput.HorizontalMove != 0.0f)
        {
            goalRotation = PlayerInput.HorizontalMove > 0.0f ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(-Vector3.right);
        }
        Rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, config.turnRate * Time.deltaTime);
    }
}
