using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;


    private float speed = 5.0f;
    private int desiredLane = 1;

    private CharacterController myCharacterController;

    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

        // Calculate future position
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.z = speed;

        // Move player
        myCharacterController.Move(moveVector * Time.deltaTime);
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

}
