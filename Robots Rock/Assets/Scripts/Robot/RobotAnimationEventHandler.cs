using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationEventHandler : MonoBehaviour
{
    public void StartJumpRoutine()
    {
        transform.parent.SendMessage("StartJumpRoutine");
    }
    public void JumpEnded()
    {
        transform.parent.SendMessage("JumpEnded");
    }
    public void PunchActivated()
    {
        transform.parent.SendMessage("PunchActivated");
    }
    public void PunchDeactivated()
    {
        transform.parent.SendMessage("PunchDeactivated");
    }
    public void PunchEnded()
    {
        transform.parent.SendMessage("PunchEnded");
    }
}
