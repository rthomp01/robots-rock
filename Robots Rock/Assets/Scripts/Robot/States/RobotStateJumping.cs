using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateJumping : IState
{
    public RobotController Owner { get; private set; }
    public RobotStateJumping(RobotController controller) { Owner = controller; }

    public void Enter()
    {
        Debug.Log("Entered jumping state.");
        Owner.Rigidbody.velocity = Vector3.zero;
        Owner.SnapToGoalRotation();
        Owner.animator.SetTrigger("Jump");
    }

    public void Execute()
    {
    }

    public void ExecuteFixed()
    {
    }

    public void Exit()
    {
        Debug.Log("Exited jumping state.");
    }
}
