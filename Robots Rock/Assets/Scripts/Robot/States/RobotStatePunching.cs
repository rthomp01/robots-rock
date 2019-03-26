using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStatePunching : IState
{
    public RobotController Owner { get; private set; }
    public RobotStatePunching(RobotController controller) { Owner = controller; }

    public void Enter()
    {
        Debug.Log("Entered punching state.");

        Owner.Rigidbody.velocity = Vector3.zero;
        Owner.animator.SetTrigger("Punch");
    }

    public void Execute()
    {

    }

    public void ExecuteFixed()
    {

    }

    public void Exit()
    {
        Debug.Log("Exited punching state.");
    }
}
