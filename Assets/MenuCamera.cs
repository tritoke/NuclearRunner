using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCamera : MonoBehaviour
{

    public ParticleSystem particleSystem;
    public GameObject light;

    public Animator anim1;

    public Animator anim2;

    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start() {
        canvas.SetActive(false);
        anim1.SetTrigger("Go");
        anim2.SetTrigger("Gogo 0");
        StartCoroutine(MoveLevel());
    }

    public void goLight() {
        light.SetActive(true);
    }

    public void enable() {
        particleSystem.Play();
    }

    public void quit() {
        Application.Quit();
    }

    IEnumerator MoveLevel() {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(0);
    } 

}
