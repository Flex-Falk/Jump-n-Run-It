using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private CharacterController controller;
    //private Vector3 direction;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private bool isGrounded = true;
    private bool isCrouching = false;
    [SerializeField]
    private float maxSpeed = 6;
    [SerializeField]
    private float initialSpeed = 3f;
    private float currentSpeed = 0;
    private float desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes
    private Vector3 velocity = Vector3.zero;
    //private Vector3 eulerAngleVelocity;
    [SerializeField]
    private float jumpForce = 0.5f;
    //public float gravity = -20;
    public Animator animator;

    private AudioSource audioSource;

    public AudioClip jumpClip;
    public AudioClip beginCrouchClip;
    public AudioClip endCrouchClip;
    public AudioClip deathClip;

    public static event Action<bool> isGameOver;

    private EventDataHook eventDataHook;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //controller = GetComponent<CharacterController>();
        cc = GetComponent<CapsuleCollider>();

        if(PortDataAccessor.Instance != null && PortDataAccessor.Instance.EventDataHook != null)
        {
            eventDataHook = PortDataAccessor.Instance.EventDataHook;

            //example Serial.print("left:1;") , important: no newline
            eventDataHook.registerDataHook("Left", (object sender, DataArrivedEventArgs args) => {
                runOnLane(0);
            });

            //example Serial.print("middle:1;") , important: no newline
            eventDataHook.registerDataHook("Middle", (object sender, DataArrivedEventArgs args) => {
                runOnLane(1);
            });

            //example Serial.print("right:1;") , important: no newline
            eventDataHook.registerDataHook("Right", (object sender, DataArrivedEventArgs args) => {
                runOnLane(2);
            });

        }

        animator.SetBool("isGameStarted", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 globalpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        if (InputHandler.PlayerRunInput())
        {
            runOnLane(InputHandler.LaneInput());
            /*
            if (currentSpeed == 0)
            {
                currentSpeed += initialSpeed; 
            }
            else
            {
                currentSpeed += 1f;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            }

            //direction.z = currentSpeed*Time.deltaTime;
            desiredLane = InputHandler.LaneInput();
            */
        }
        else
        {
            currentSpeed -= 0.01f;
            if (currentSpeed <= 0.01)
                currentSpeed = 0;
            //direction.z = currentSpeed*Time.deltaTime;
            
        }

        animator.SetBool("isJumping", !isGrounded);
        if (InputHandler.JumpInput())
        {
            Debug.Log(isGrounded);    
            if (isGrounded)
            {
                Jump();
                isGrounded = false;
            }
         }

        animator.SetBool("isCrouching", isCrouching);
        if (InputHandler.CrouchInput())
        {
            //eulerAngleVelocity.Set(0f, 0f, 90);
            StartCoroutine(Crouch());
            isCrouching = true;
        }

        /*else
        {
            //direction.y += gravity * Time.deltaTime;
        }*/
        //controller.Move(direction);
        globalpos.z += currentSpeed;
        //Debug.Log(currentSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, globalpos, ref velocity, 0.3f);

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

    void runOnLane(float lane)
    {
        if (currentSpeed == 0)
        {
            currentSpeed += initialSpeed;
        }
        else
        {
            currentSpeed += 1f;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }

        //direction.z = currentSpeed*Time.deltaTime;
        desiredLane = lane;
    }

    private void Jump()
    {
        rb.velocity += jumpForce * Vector3.up;
        audioSource.PlayOneShot(jumpClip);
    }

    private IEnumerator Crouch()
    {
        //Aktuell gel�st durch �nderung der H�he
        audioSource.PlayOneShot(beginCrouchClip);

        cc.height = 1;
        yield return new WaitForSeconds(1.5f);
        cc.height = 2;
        isCrouching = false;
        audioSource.PlayOneShot(endCrouchClip);
        /*var normScale = new Vector3(1f, 1f, 1f);
        var crouchScale = new Vector3(0.6f, 0.5f, 0.6f);
        transform.localScale = Vector3.Lerp(transform.localScale, crouchScale, 80 * Time.fixedDeltaTime);
        yield return new WaitForSeconds(1.5f);
        transform.localScale = Vector3.Lerp(transform.localScale, normScale, 80 * Time.fixedDeltaTime);
        */
    }

    private void OnCollisionStay()
    {
         isGrounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            isGameOver?.Invoke(true);
            DisableControls();
        }
    }

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((hit.transform.tag == "Obstacle"))
        {
            isGameOver?.Invoke(true);
        }
    }
    */

     public void DisableControls()
    {
        this.enabled = false;
        audioSource.PlayOneShot(deathClip);
    }
}
