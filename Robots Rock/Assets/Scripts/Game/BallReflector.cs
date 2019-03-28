using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallReflector : MonoBehaviour
{
    public float reflectPower = 3.0f;
    public bool setToReflectorLayer = true;

    public CameraShakeEvent cameraShakeEvent;
    public float xShake;
    public float yShake;
    public float shakeDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Reflect(other.GetComponent<Rigidbody>());
        }
    }

    void Reflect(Rigidbody target)
    {
        target.velocity = -target.velocity * reflectPower;

        if(setToReflectorLayer)
        {
            target.gameObject.layer = gameObject.layer;
        }

        if(cameraShakeEvent != null)
        {
            cameraShakeEvent.Invoke(shakeDuration, xShake, yShake);
        }
    }
}
