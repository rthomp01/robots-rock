using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    public FiniteStateMachine StateMachine { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput PlayerInput { get; set; }

    //set in inspector
    public Animator animator;
    public ControllerConfig config;

    //private
    private Quaternion goalRotation;

    private void Awake()
    {
        if (animator == null || config == null)
        {
            Debug.LogFormat("Animator or ControllerConfig references missing on {0}!", transform.name);
            this.enabled = false;
            return;
        }

        Rigidbody = GetComponent<Rigidbody>();
        PlayerInput = new PlayerInput();

        goalRotation = transform.rotation;

        //default player state is grounded
        StateMachine = new FiniteStateMachine();
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

    //Triggered by event during jump animation clip
    public void StartJumpRoutine()
    {
        StartCoroutine(JumpRoutine());
    }

    //Triggered by event at the end of jump animation clip
    public void JumpEnded()
    {
        StateMachine.ChangeState(new RobotStateGrounded(this));
    }

    //Jump handled by parabola lerp. airborneDuration is used to match the time spent in air
    //with the visuals of the jump animation provided. height/distance/duration configurable 
    //in ControllerConfig.
    IEnumerator JumpRoutine()
    {
        transform.rotation = goalRotation;

        Vector3 goal = transform.position + transform.forward * config.jumpDistance;
        Vector3 origin = transform.position;

        float timer = 0.0f;
        while (timer <= 1.0f)
        {
            float height = Mathf.Sin(Mathf.PI * timer) * config.jumpHeight;
            Rigidbody.MovePosition(Vector3.Lerp(origin, goal, timer) + Vector3.up * height);

            timer += Time.deltaTime / config.airborneDuration;
            yield return null;
        }
    }
}
