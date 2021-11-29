using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundRandomiser : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;

    private AudioSource source;
    private Random rng;

    void Start()
    {
        source = GetComponent<AudioSource>();
        rng = new Random();
    }

    public void PlayRandom()
    {
        var index = rng.Next(clips.Count);
        var clip = clips[index];
        source.PlayOneShot(clip);
    }

}
