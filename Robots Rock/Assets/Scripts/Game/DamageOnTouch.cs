using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnTouch : MonoBehaviour
{
    public List<string> ignoreTags = new List<string>();
    public ParticleSystem onHitEffect;

    public int damageAmount = 1;
    public bool disableOnHit = true;

    public AudioClip onHitSFX;

    private void Start()
    {
        if (onHitEffect != null)
        {
            onHitEffect = Instantiate<ParticleSystem>(onHitEffect, transform.position, transform.rotation);
            onHitEffect.transform.SetParent(transform.parent);
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
        }

        Hit(other.gameObject);
    }

    void Hit(GameObject other)
    {
        PlayHitEffect();

        //If two projectiles collide we can play both hit effects and
        //cancel eachother out.
        DamageOnTouch d = other.GetComponent<DamageOnTouch>();
        if(d != null)
        {
            d.PlayHitEffect();
            if(d.disableOnHit)
            {
                d.gameObject.SetActive(false);
            }
        }

        if (disableOnHit)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayHitEffect()
    {
        if(onHitSFX != null)
        {
            FindObjectOfType<SFXPlayer>().PlayOneShotWithVariedPitch(onHitSFX);
        }

        if (onHitEffect != null)
        {
            onHitEffect.transform.SetPositionAndRotation(transform.position, transform.rotation);
            onHitEffect.Play();
        }
    }
}
