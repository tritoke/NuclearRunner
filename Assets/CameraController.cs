using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Rotation Properties")]
    float temp;
    bool isRotating;
    int horizontalDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void checkRotate()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = -1;
            temp = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = 1;
            temp = 0;
        }

        transform.Rotate(Vector3.up * 90 * Time.fixedDeltaTime * horizontalDirection, Space.World);

        temp += 90 * Time.fixedDeltaTime;
        if (temp >= 90)
        {
            temp = 0;
            horizontalDirection = 0;
            isRotating = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checkRotate();
    }
}
