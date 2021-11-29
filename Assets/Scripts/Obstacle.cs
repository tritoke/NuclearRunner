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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // pause the engine sounds
            var player = collision.gameObject.transform.parent.GetChild(0);
            player.GetComponent<AudioSource>().Pause();

            // play our sounds
            StartCoroutine(playSound());

            Time.timeScale = 0.0f;
            var canvas = GameObject.FindWithTag("Game Over Canvas");
            var innerCanvas = canvas.GetComponent<Canvas>();
            innerCanvas.enabled = true;

            var scoreText = canvas.transform.Find("ScoreText").gameObject;
            var scoreTextInner = scoreText.GetComponent<Text>();
            scoreTextInner.text = $"score {Score}";
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

    public static void ResetScore()
    {
        Score = 0;
    }

    void OnDestroy()
    {
        var pcr = FindObjectOfType<PlayerController>();
        if (pcr != null && transform.position.z < pcr.transform.position.z)
        {
            Score++;
            pcr.speed++;
        }
    }
}
