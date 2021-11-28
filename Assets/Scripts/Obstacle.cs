using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static int NumDeleted { get; private set; } = 0;

    public AudioSource crashSound;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //game over
            crashSound.Play();
            Debug.Log("GAME OVER");
            Time.timeScale = 0.0f;
        }
    }

    void OnDelete()
    {
        NumDeleted++;
    }
}