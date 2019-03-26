using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReflector : MonoBehaviour
{
    public float reflectPower = 3.0f;
    public bool setToReflectorLayer = true;

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
    }
}
