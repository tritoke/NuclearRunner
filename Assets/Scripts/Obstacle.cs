using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    private static int Score;

    public AudioSource crashSound;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //game over
            crashSound.Play();
            Debug.Log("GAME OVER");

            Time.timeScale = 0.0f;
            var canvas = GameObject.FindGameObjectsWithTag("Game Over Canvas")[0];
            var innerCanvas = canvas.GetComponent<Canvas>();
            innerCanvas.enabled = true;

            var scoreText = canvas.transform.Find("ScoreText").gameObject;
            var scoreTextInner = scoreText.GetComponent<Text>();
            scoreTextInner.text = $"score {Score}";
        }
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
        }
    }
}