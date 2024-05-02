using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private float desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes

    public float jumpForce;
    public float gravity = -20;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        controller.Move(direction * Time.deltaTime);

        if (controller.isGrounded) 
        {
            //direction.y = -1;
            if (InputHandler.JumpInput())
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }


        //Gather the inputs on what lane we should be
        /*if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if(desiredLane == -1)
            {
                desiredLane = 0;
            }
        }*/
        if (InputHandler.ChangeDirectionInput())
        {
            desiredLane = desiredLane + InputHandler.DirectionInput();
    
            //Fixes out of bound issues
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }else if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }


        //Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
    }


    private void Jump()
    {
        direction.y = jumpForce;
    }
}
