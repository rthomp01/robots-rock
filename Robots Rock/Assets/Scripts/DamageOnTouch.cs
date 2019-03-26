using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int damageAmount = 1;
    public bool disableOnHit = true;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable d = other.GetComponent<IDamageable>();
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
    }
}
