using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    //Set in inspector
    public Rigidbody pitchablePrefab;
    public Transform spawnTransform;
    public PitcherConfig config;
    //

    public bool IsPitching { get; private set; }

    private void Start()
    {
        IsPitching = true;
        StartCoroutine(PitchRoutine());
    }

    IEnumerator PitchRoutine()
    {
        while(IsPitching)
        {
            Rigidbody rb = Instantiate<Rigidbody>(pitchablePrefab, spawnTransform.position, spawnTransform.rotation);
            rb.velocity = spawnTransform.forward * config.pitchSpeed;
            yield return new WaitForSeconds(config.pitchFrequency);
        }
    }
}
