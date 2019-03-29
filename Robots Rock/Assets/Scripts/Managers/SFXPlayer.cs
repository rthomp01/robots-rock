using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("SFXPlayer could not play because the supplied AudioClip was not set.");
            return;
        }

        source.pitch = Random.Range(0.95f, 1.05f);
        source.clip = clip;
        source.PlayOneShot(clip);
    }
}
