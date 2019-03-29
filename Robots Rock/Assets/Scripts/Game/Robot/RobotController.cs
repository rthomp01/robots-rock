using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    public FiniteStateMachine StateMachine { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput PlayerInput { get; set; }

    //set in inspector
    public Animator animator;
    [Tooltip("Use Create->Configuration->Controller")]
    public ControllerConfig config;
    public SphereCollider punchCollider;
    public ParticleSystem footstepEffect;
    public ParticleSystem jumpLandingEffect;
    public Transform leftFootTransform;
    public Transform rightFootTransform;

    [Space()]
    [Header("SFX")]
    public SFXPlayer sfxPlayer;
    public AudioClip footstepClip;
    public AudioClip punchClip;

    [Space()]
    [Header("Controller Events")]
    public UnityEvent robotJumpedEvent;
    public UnityEvent robotLandedEvent;

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

        if (footstepEffect != null)
        {
            footstepEffect = Instantiate<ParticleSystem>(footstepEffect, transform.position, Quaternion.identity);
        }
        if (jumpLandingEffect != null)
        {
            jumpLandingEffect = Instantiate<ParticleSystem>(jumpLandingEffect, transform.position, Quaternion.identity);
        }
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

    public void SnapToGoalRotation()
    {
        transform.rotation = goalRotation;
    }

    /// <summary>
    /// Triggered by an event during the Jump_Modified animation clip. Used to time
    /// vertical motion in sync with the animation provided.
    /// </summary>
    public void StartJumpRoutine()
    {
        StartCoroutine(JumpRoutine());
    }

    /// <summary>
    /// Triggered by an event at the end of the Jump_Modified animation clip. Used to 
    /// signal the end of the jump and change states back to grounded.
    /// </summary>
    public void JumpEnded()
    {
        StateMachine.ChangeState(new RobotStateGrounded(this));
    }

    /// <summary>
    /// Triggered by an event during the NormalPunch_Modified animation clip. Used to time
    /// the enabling of the punch collider so that it's only active during a small window 
    /// throughout the animation.
    /// </summary>
    public void PunchActivated()
    {
        if (punchCollider)
        {
            punchCollider.enabled = true;
        }
    }

    /// <summary>
    /// Triggered by an event during the NormalPunch_Modified animation clip. Used to time
    /// the enabling of the punch collider so that it's only active during a small window 
    /// throughout the animation.
    /// </summary>
    public void PunchDeactivated()
    {
        if (punchCollider)
        {
            punchCollider.enabled = false;
        }
    }

    /// <summary>
    /// Triggered by event at the end of the NormalPunch_Modified animation clip. Used 
    /// to signal the end of the punch and change states back to grounded.
    /// </summary>
    public void PunchEnded()
    {
        StateMachine.ChangeState(new RobotStateGrounded(this));
    }

    /// <summary>
    /// Triggered by event during the NormalPunch_Modified animation clip. Used 
    /// to signal the timing for the punch SFX to be triggered.
    /// </summary>
    public void PlayPunchSFX()
    {
        if (punchClip != null)
        {
            sfxPlayer.PlayOneShot(punchClip);
        }
    }

    /// <summary>
    /// Triggered by an event during the walk animation so we can time effects.
    /// </summary>
    public void Footstep(string foot)
    {
        if (footstepClip != null)
        {
            sfxPlayer.PlayOneShot(footstepClip);
        }

        if (foot == "Left")
        {
            footstepEffect.transform.position = leftFootTransform.position;
            footstepEffect.transform.rotation = leftFootTransform.rotation;
        }
        else
        {
            footstepEffect.transform.position = rightFootTransform.position;
            footstepEffect.transform.rotation = rightFootTransform.rotation;
        }

        footstepEffect.Play();
    }

    /// <summary>
    /// Jump handled by parabolic lerp. airborneDuration is used to match the time spent in air
    /// with the visuals of the jump animation provided. height/distance/duration configurable
    /// in ControllerConfig.
    /// </summary>
    IEnumerator JumpRoutine()
    {
        if (robotJumpedEvent != null)
        {
            robotJumpedEvent.Invoke();
        }

        Vector3 goal = transform.position + transform.forward * config.jumpDistance;
        Vector3 origin = transform.position;

        float timer = 0.0f;
        while (timer <= 1.0f)
        {
            float height = Mathf.Sin(Mathf.PI * timer) * config.jumpHeight;
            Rigidbody.MovePosition(Vector3.Lerp(origin, goal, timer) + Vector3.up * height);

            timer += Time.deltaTime / config.airborneDuration;
            yield return new WaitForFixedUpdate();
        }

        if (robotLandedEvent != null)
        {
            robotLandedEvent.Invoke();
        }

        jumpLandingEffect.transform.position = transform.position;
        jumpLandingEffect.Play();
    }
}
