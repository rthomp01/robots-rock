using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private Coroutine pitchRoutine;

    public UnityEvent pitchEvent;

    private void OnEnable()
    {
        IsPitching = true;
        pitchRoutine = StartCoroutine(PitchRoutine());
    }

    private void OnDisable()
    {
        IsPitching = false;
        StopCoroutine(pitchRoutine);
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

            if(pitchEvent != null)
            {
                pitchEvent.Invoke();
            }

            yield return new WaitForSeconds(config.pitchFrequency);
        }
    }
}
