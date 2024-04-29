using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes

    public float jumpForce;
    public float gravity = -20;


    private PortDataAccessor portDataAccessor;
    private EventHandler<DataArrivedEventArgs> onJump, onRight, onLeft;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        portDataAccessor = PortDataAccessor.Instance;

        onJump = (object sender, DataArrivedEventArgs args) =>
        {
            Debug.Log(String.Format("Key: {0}, Value: {1}", args.Key, args.Value));

            if (controller.isGrounded)
            {
                Jump();
            }
        };

        onLeft = (object sender, DataArrivedEventArgs args) =>
        {
            Debug.Log(String.Format("Key: {0}, Value: {1}", args.Key, args.Value));

            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        };

        onRight = (object sender, DataArrivedEventArgs args) =>
        {
            Debug.Log(String.Format("Key: {0}, Value: {1}", args.Key, args.Value));

            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        };

        // Note: you can register multiple handler for the same key
        portDataAccessor.EventDataHook.registerDataHook("jump", onJump);
        portDataAccessor.EventDataHook.registerDataHook("left", onLeft);      
        portDataAccessor.EventDataHook.registerDataHook("right", onRight);
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        controller.Move(direction * Time.deltaTime);

        // skip gravity for one frame to match behaviour in previous version
        if (isLastFrameJumped)
        {
            isLastFrameJumped = false;
        }
        else
        {
            direction.y += gravity * Time.deltaTime;    
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

    private bool isLastFrameJumped = false;
    private void Jump()
    {
        isLastFrameJumped = true;
        direction.y = jumpForce;
    }

    private void OnDestroy()
    {
        portDataAccessor.EventDataHook.unregisterDataHook("jump", onJump);
        portDataAccessor.EventDataHook.unregisterDataHook("left", onLeft);
        portDataAccessor.EventDataHook.unregisterDataHook("right", onRight);
    }
}
