using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationEventHandler : MonoBehaviour
{
    public RobotController controller;

    public void StartJumpRoutine()
    {
        controller.StartJumpRoutine();
    }
    public void JumpEnded()
    {
        controller.JumpEnded();
    }
    public void PunchActivated()
    {
        controller.PunchActivated();
    }
    public void PunchDeactivated()
    {
        controller.PunchDeactivated();
    }
    public void PunchEnded()
    {
        controller.PunchEnded();
    }
    public void PunchSFX()
    {
        controller.PlayPunchSFX();
    }
}
