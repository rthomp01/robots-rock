using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallReflector : MonoBehaviour
{
    public float reflectPower = 3.0f;
    public bool setToReflectorLayer = true;

    public UnityEvent onReflectEvent;

    public ParticleSystem onReflectEffect;

    private void Awake()
    {
        if (onReflectEffect != null)
        {
            onReflectEffect = Instantiate(onReflectEffect, transform.position, Quaternion.identity);
            onReflectEffect.transform.SetParent(transform);
        }
    }

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

        if (setToReflectorLayer)
        {
            target.gameObject.layer = gameObject.layer;
        }

        if (onReflectEvent != null)
        {
            onReflectEvent.Invoke();
        }

        PlayReflectEffect();
    }

    void PlayReflectEffect()
    {
        if (onReflectEffect != null)
        {
            onReflectEffect.transform.position = transform.position;
            onReflectEffect.Play();
        }
    }
}
