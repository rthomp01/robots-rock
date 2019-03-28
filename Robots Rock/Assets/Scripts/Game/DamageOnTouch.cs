using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public List<string> ignoreTags = new List<string>();
    public ParticleSystem onHitEffect;

    public int damageAmount = 1;
    public bool disableOnHit = true;

    private void Awake()
    {
        if (onHitEffect != null)
        {
            onHitEffect = Instantiate<ParticleSystem>(onHitEffect, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ignoreTags.Contains(other.tag))
            return;

        IDamageable d = other.transform.root.GetComponent<IDamageable>();

        if (d != null)
        {
            d.TakeDamage(damageAmount);

            Debug.LogFormat("{0} hit {1} for {2} damage!", transform.name,
                other.transform.name, damageAmount);

            if (disableOnHit)
            {
                gameObject.SetActive(false);
            }
        }

        PlayOnHitEffect();
    }

    void PlayOnHitEffect()
    {
        if (onHitEffect != null)
        {
            onHitEffect.transform.position = transform.position;
            onHitEffect.transform.rotation = transform.rotation;
            onHitEffect.Play();
        }
    }
}
