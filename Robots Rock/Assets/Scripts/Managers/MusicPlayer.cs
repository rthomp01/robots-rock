using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioClip titleMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameoverMusic;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySong(string song)
    {
        switch (song)
        {
            case "gameplay":
                source.loop = true;
                source.clip = gameplayMusic;
                break;
            case "gameover":
                source.loop = false;
                source.clip = gameoverMusic;
                break;
        }

        source.Play();
    }
}
