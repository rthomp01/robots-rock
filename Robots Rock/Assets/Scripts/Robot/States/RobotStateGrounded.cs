using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateGrounded : IState
{
    public RobotController Owner { get; private set; }
    public RobotStateGrounded(RobotController controller) { Owner = controller; }

    private float xMove;
    private float inputVelocity;

    public void Enter()
    {
        Debug.Log("Entered grounded state.");
    }

    public void Execute()
    {
        xMove = Mathf.SmoothDamp(xMove, Owner.PlayerInput.HorizontalMove, ref inputVelocity, Owner.config.inputDampening);
    }

    public void ExecuteFixed()
    {
        Owner.Rigidbody.velocity = new Vector3(xMove * Owner.config.walkSpeed,
            Owner.Rigidbody.velocity.y, Owner.Rigidbody.velocity.z);

        UpdateAnimator();
    }

    public void UpdateAnimator()
    {
        if (Owner.animator == null)
            return;

        Owner.animator.SetFloat("WalkSpeed", Mathf.Abs(xMove));
    }

    public void Exit()
    {
        Debug.Log("Exited grounded state.");
    }
}
