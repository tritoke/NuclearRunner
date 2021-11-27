using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{

    public ParticleSystem particleSystem;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goLight() {
        light.SetActive(true);
    }

    public void enable() {
        particleSystem.Play();
    }
}
