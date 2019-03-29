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
    public int poolAmount;
    public GameObject objectPoolContainer;

    [Space()]
    [Tooltip("Use Create->Configuration->Pitcher")]
    public PitcherConfig config;

    [Tooltip("Spawn transform for pitched object.")]
    public Transform spawnTransform;

    [Space()]
    [Tooltip("This event is triggered when the pitcher fires a ball of any type.")]
    public UnityEvent pitchEvent;
    //

    public bool IsPitching { get; private set; }

    private Coroutine pitchRoutine;
    private Dictionary<ProjectileConfig, List<Rigidbody>> projectiles = new Dictionary<ProjectileConfig, List<Rigidbody>>();

    private void Awake()
    {
        foreach (ProjectileConfig config in pitchableObjects)
        {
            List<Rigidbody> projectileRBs = new List<Rigidbody>();
            for (int i = 0; i < poolAmount; i++)
            {
                Rigidbody rb = Instantiate<Rigidbody>(config.prefab, spawnTransform.position, spawnTransform.rotation);

                if (objectPoolContainer != null)
                    rb.gameObject.transform.SetParent(objectPoolContainer.transform);
                else
                    rb.gameObject.transform.SetParent(transform);

                rb.gameObject.SetActive(false);
                projectileRBs.Add(rb);
            }
            if (!projectiles.ContainsKey(config))
            {
                projectiles.Add(config, projectileRBs);
            }
        }
    }

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
        ProjectileConfig projectileConfig;
        float pitchSpeedModifier;

        while (IsPitching)
        {
            projectileConfig = pitchableObjects[Random.Range(0, pitchableObjects.Length)];
            pitchSpeedModifier = Random.Range(config.minPitchSpeedModifier, config.maxPitchSpeedModifier);

            for (int i = 0; i < projectiles[projectileConfig].Count; i++)
            {
                if (!projectiles[projectileConfig][i].gameObject.activeInHierarchy)
                {
                    Rigidbody rb = projectiles[projectileConfig][i];
                    rb.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                    rb.velocity = spawnTransform.forward * projectileConfig.baseSpeed * pitchSpeedModifier;
                    rb.gameObject.layer = gameObject.layer;
                    rb.gameObject.SetActive(true);
                    break;
                }
            }

            if (pitchEvent != null)
            {
                pitchEvent.Invoke();
            }

            yield return new WaitForSeconds(config.pitchFrequency);
        }
    }
}
