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

    /// <summary>
    /// While grounded the Robot can move, jump or attack.
    /// </summary>
    public void Execute()
    {
        xMove = Mathf.SmoothDamp(xMove, Owner.PlayerInput.HorizontalMove, ref inputVelocity, Owner.config.inputDampening);
        UpdateAnimator();

        if (Owner.PlayerInput.JumpPressed)
        {
            Owner.StateMachine.ChangeState(new RobotStateJumping(Owner));
            return;
        }
        else if(Owner.PlayerInput.AttackPressed)
        {
            Owner.StateMachine.ChangeState(new RobotStatePunching(Owner));
            return;
        }
    }

    public void ExecuteFixed()
    {
        Owner.Rigidbody.velocity = new Vector3(xMove * Owner.config.walkSpeed,
            Owner.Rigidbody.velocity.y, Owner.Rigidbody.velocity.z);

        Owner.UpdateRotation();
    }

    public void UpdateAnimator()
    {
        Owner.animator.SetFloat("WalkSpeed", Mathf.Abs(xMove));
    }

    public void Exit()
    {
        Debug.Log("Exited grounded state.");
    }
}
