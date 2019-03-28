using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    //Set in inspector
    [Tooltip("Everytime a pitch is thrown it will be randomly chosen from this list." +
        "Create new objects using Create->Objects->Projectile")]
    public ProjectileConfig[] pitchableObjects;

    [Space()]
    [Tooltip("Use Create->Configuration->Pitcher")]
    public PitcherConfig config;

    [Tooltip("Spawn transform for pitched object.")]
    public Transform spawnTransform;
    //

    public bool IsPitching { get; private set; }

    private void Start()
    {
        IsPitching = true;
        StartCoroutine(PitchRoutine());
    }

    IEnumerator PitchRoutine()
    {
        int prefabIndex;
        float pitchSpeedModifier;

        while (IsPitching)
        {
            prefabIndex = Random.Range(0, pitchableObjects.Length);
            pitchSpeedModifier = Random.Range(config.minPitchSpeedModifier, config.maxPitchSpeedModifier);

            Rigidbody rb = Instantiate<Rigidbody>(pitchableObjects[prefabIndex].prefab,
                spawnTransform.position, spawnTransform.rotation);
     
            rb.velocity = spawnTransform.forward * pitchableObjects[prefabIndex].baseSpeed * pitchSpeedModifier;

            yield return new WaitForSeconds(config.pitchFrequency);
        }
    }
}
