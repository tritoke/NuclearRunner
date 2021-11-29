using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
