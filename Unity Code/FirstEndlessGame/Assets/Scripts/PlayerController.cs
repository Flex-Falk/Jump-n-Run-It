using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider cc;
    private bool isGrounded = true;

    private bool isCurrentlyInAction = false;
    private bool isCrouching = false;
    [SerializeField]
    private float maxSpeed = 6;
    [SerializeField]
    private float initialSpeed = 3f;
    private float currentSpeed = 0;
    private float speedUp = 4f;
    private float desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes
    private Vector3 velocity = Vector3.zero;
    //private Vector3 eulerAngleVelocity;
    [SerializeField]
    private float jumpForce = 0.5f;
    public Animator animator;

    public Transform airShotSpawnPoint;
    public GameObject airShotPrefab;


    private AudioSource audioSource;

    public AudioClip jumpClip;
    public AudioClip beginCrouchClip;
    public AudioClip endCrouchClip;
    public AudioClip deathClip;
    public AudioClip attackClip;


    public static event Action<bool> isGameOver;

    private EventDataHook eventDataHook;
    private List<GyroData> gyroData = new List<GyroData>();

    private UdpSocket udpSocket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        cc = GetComponent<CapsuleCollider>();

        udpSocket = UdpSocket.Instance;

        if (PortDataAccessor.Instance != null && PortDataAccessor.Instance.EventDataHook != null)
        {
            eventDataHook = PortDataAccessor.Instance.EventDataHook;

            //example Serial.print("left:1;") , important: no newline
            eventDataHook.registerDataHook("Left", (object sender, DataArrivedEventArgs args) =>
            {
                runOnLane(0);
            });

            //example Serial.print("middle:1;") , important: no newline
            eventDataHook.registerDataHook("Middle", (object sender, DataArrivedEventArgs args) =>
            {
                runOnLane(1);
            });

            //example Serial.print("right:1;") , important: no newline
            eventDataHook.registerDataHook("Right", (object sender, DataArrivedEventArgs args) =>
            {
                runOnLane(2);
            });
            //
            eventDataHook.registerDataHook("IMU", (object sender, DataArrivedEventArgs args) =>
            {
                Debug.Log("[IMU] " + args.Value);
                udpSocket.SendData(args.Value);
                gyroData.Add(new GyroData(args.Value));
            });
            /*
            eventDataHook.registerDataHook("Accel", (object sender, DataArrivedEventArgs args) =>
            {
                Debug.Log("Accel" + args.Value);
                if (Input.GetKey(KeyCode.Space))
                {
                    args.Value += ",Jump";
                } else if (Input.GetKey(KeyCode.C)) {
                    args.Value += ",Crouch";
                }else if (Input.GetKey(KeyCode.X)) {
                    args.Value += ",Shoot";
                } else {
                    args.Value += ",Neutral";
                }
                gyroData.Add(new GyroData(args.Key, Time.time, args.Value));
            });

            eventDataHook.registerDataHook("Gyro", (object sender, DataArrivedEventArgs args) =>
            {
                Debug.Log("Gyro" + args.Value);
                if (Input.GetKey(KeyCode.Space))
                {
                    args.Value += ",Jump";
                } else if (Input.GetKey(KeyCode.C)) {
                    args.Value += ",Crouch";
                }else if (Input.GetKey(KeyCode.X)) {
                    args.Value += ",Shoot";
                } else {
                    args.Value += ",Neutral";
                }
                gyroData.Add(new GyroData(args.Key, Time.time, args.Value));
            });
            */

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
        }
        else
        {
            currentSpeed -= 0.01f;
            if (currentSpeed <= 0.01)
                currentSpeed = 0;
            //direction.z = currentSpeed*Time.deltaTime;

        }


        if (InputHandler.JumpInput())
        {
            Debug.Log("[Grounded] " + isGrounded);
            if (isGrounded | PlayerManager.doubleJumpPowerUp == true && isCurrentlyInAction == false)
            {
                StartCoroutine(Jump());
                isGrounded = false;
                PlayerManager.doubleJumpPowerUp = false;
            }
        }

        animator.SetBool("isCrouching", isCrouching);
        if (InputHandler.CrouchInput())
        {
            //eulerAngleVelocity.Set(0f, 0f, 90);
            StartCoroutine(Crouch());
            isCrouching = true;
        }

        if (InputHandler.AttackInput())
        {
            Attack();
        }

        /*else
        {
            //direction.y += gravity * Time.deltaTime;
        }*/
        //controller.Move(direction);
        globalpos.z += currentSpeed;
        transform.position = Vector3.SmoothDamp(transform.position, globalpos, ref velocity, 0.3f);
        //Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
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
            if (PlayerManager.speedPowerUp)
            {
                currentSpeed += speedUp;
                PlayerManager.speedPowerUp = false;
                currentSpeed = Mathf.Min(currentSpeed, speedUp);
            }
            else
            {
                currentSpeed += 1f;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            }


        }

        //direction.z = currentSpeed*Time.deltaTime;
        desiredLane = lane;
    }

    private IEnumerator Jump()
    {
        if(isCurrentlyInAction == false && isGrounded == true){
            isCurrentlyInAction = true;
            
            animator.SetBool("isJumping", true);
            rb.velocity += jumpForce * Vector3.up;
            audioSource.PlayOneShot(jumpClip);
            yield return new WaitForSeconds(1f);

            isCurrentlyInAction = false;
        }
    }

    private IEnumerator Crouch()
    {
        if(isCurrentlyInAction == false){
            isCurrentlyInAction = true;
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
            isCurrentlyInAction = false;
        }
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Breakable")
        {
            if (!PlayerManager.Instance.HitShield())
            {
                isGameOver?.Invoke(true);
                DisableControls();
            }

        }
        if (collision.transform.tag == "Ground")
        {
            animator.SetBool("isJumping", false);
        }
    }
    public void DisableControls()
    {
        this.enabled = false;
        audioSource.PlayOneShot(deathClip);
        SaveSystem.SaveGyroData(gyroData);
    }

    void Attack()
    {
        if(isCurrentlyInAction == false){
            isCurrentlyInAction = true;
            Debug.Log("[Action] Shoot");

            var airShot = Instantiate(airShotPrefab, airShotSpawnPoint.position, airShotPrefab.transform.rotation);
            airShot.GetComponent<Rigidbody>().velocity = airShotSpawnPoint.forward * 20f;
            audioSource.PlayOneShot(attackClip);
            isCurrentlyInAction = false;
        }
    }
}
