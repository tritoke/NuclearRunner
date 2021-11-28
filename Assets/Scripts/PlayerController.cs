using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.05f;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float jumpForce = 8.0f;

    [SerializeField]
    private float gravity = 20.0f;

    private float verticalVelocity;
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

        // Calculate y value
        if (myCharacterController.isGrounded)
        {
            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Jump
                verticalVelocity = jumpForce;
            }
        } 
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = -jumpForce;
            }
        }
        moveVector.y = verticalVelocity;

        moveVector.z = speed;

        // Move player
        myCharacterController.Move(moveVector * Time.deltaTime);

        // Rotate to where they're going
        Vector3 dir = myCharacterController.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

}
