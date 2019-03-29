using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClipLooping(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("MusicPlayer could not play because the supplied AudioClip was not set.");
            return;
        }

        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    public void PlayClipOnce(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("MusicPlayer could not play because the supplied AudioClip was not set.");
            return;
        }

        source.loop = false;
        source.clip = clip;
        source.Play();
    }
}
