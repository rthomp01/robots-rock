using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateHurt : IState
{
    public RobotController Owner { get; private set; }
    public RobotStateHurt(RobotController controller) { Owner = controller; }

    public void Enter()
    {
        Debug.Log("Entered Hurt state.");
        Owner.Rigidbody.velocity = Vector3.zero;
        Owner.animator.SetTrigger("Hurt");
    }

    public void Execute()
    {

    }

    public void ExecuteFixed()
    {

    }

    public void Exit()
    {
        Debug.Log("Exited Hurt state.");
    }
}
