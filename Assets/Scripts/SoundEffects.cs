using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource backgroundMotor;

    void Start()
    {
        backgroundMotor.loop = true;

        playBackground();
    }

    public void playBackground()
    {
        backgroundMotor.Play();
    }

}
