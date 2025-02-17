using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    public AudioClip music;

    public void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }
}
