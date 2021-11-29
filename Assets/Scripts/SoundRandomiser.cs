using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundRandomiser : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        var index = Random.Range(0, clips.Count);
        var clip = clips[index];
        source.PlayOneShot(clip);
    }
}
