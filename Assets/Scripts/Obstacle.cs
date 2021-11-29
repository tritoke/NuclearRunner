using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Obstacle : MonoBehaviour
{
    private static int Score;

    [SerializeField]
    private AudioClip DeathClip;

    [SerializeField]
    private AudioClip HoldClip;

    private PlayerController PlayerControllerRef;

    void Start()
    {
        PlayerControllerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            // stop time moving forwards
            Time.timeScale = 0.0f;

            // pause the engine sounds
            var player = collision.gameObject.transform.parent.GetChild(0);
            player.GetComponent<AudioSource>().Pause();

            // play our sounds
            StartCoroutine(playSound());

            // enable the game over screen
            var canvas = GameObject.FindWithTag("Game Over Canvas");
            var innerCanvas = canvas.GetComponent<Canvas>();
            innerCanvas.enabled = true;

            // fill in the score
            var scoreText = canvas.transform.Find("ScoreText").gameObject;
            var scoreTextInner = scoreText.GetComponent<Text>();
            scoreTextInner.text = $"score {Score}";

            // reset the score
            Score = 0;
        }
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = DeathClip;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(DeathClip.length);
        GetComponent<AudioSource>().clip = HoldClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }

    void OnDestroy()
    {
        if (transform.position.z < PlayerControllerRef.transform.position.z)
        {
            Score++;
            PlayerControllerRef.speed++;
        }
    }
}
