using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{

    [Header("Player Properties")]
    public float playerSpeed = 5f;
    //public float playerJumpHeight = 1f;
    public float changeLaneSpeed = 10f;
    //public float gravity = 12f;

    [Header("Lane Properties")]
    public float laneWidth = 1.5f;
    private int laneIndex = 0;

    [Header("Rotation Properties")]
    float temp;
    bool isRotating;
    int horizontalDirection;

    private CharacterController myCharacterController;
    private Vector3 velocity;

    bool userInput = true;

    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        laneIndex = 0;
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {

        if (userInput == true)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                print("Left");
                if (laneIndex == 0 || laneIndex == 1)
                {
                    laneIndex--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                print("Right");
                if (laneIndex == 0 || laneIndex == -1)
                {
                    laneIndex++;
                }
            }
            else if (myCharacterController.isGrounded)
            {
                velocity = Vector3.forward * playerSpeed;

                //if (Input.GetKeyDown(KeyCode.UpArrow))
                //{
                //    velocity.y = Mathf.Sqrt(2 * gravity * playerJumpHeight);
                //}
            }
        }

        //velocity.y -= gravity * Time.deltaTime;

        Vector3 moveAmount = velocity * Time.deltaTime;
        float targetX = laneIndex * laneWidth;
        float dirX = Mathf.Sign(targetX - transform.position.x);
        float deltaX = changeLaneSpeed * dirX * Time.deltaTime;

        // Correct for overshoot
        if (Mathf.Sign(targetX - (transform.position.x + deltaX)) != dirX)
        {
            float overshoot = targetX - (transform.position.x + deltaX);
            deltaX += overshoot;
        }
        moveAmount.x = deltaX;

        myCharacterController.Move(moveAmount);
    }

}
