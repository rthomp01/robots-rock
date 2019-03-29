using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateDead : IState
{
    public RobotController Owner { get; private set; }
    public RobotStateDead(RobotController controller) { Owner = controller; }

    public void Enter()
    {
        Debug.Log("Entered Dead state.");
        Owner.Rigidbody.velocity = Vector3.zero;
        Owner.animator.SetTrigger("Deactivate");
    }

    public void Execute()
    {

    }

    public void ExecuteFixed()
    {

    }

    public void Exit()
    {
        Debug.Log("Exited Dead state.");
    }
}
