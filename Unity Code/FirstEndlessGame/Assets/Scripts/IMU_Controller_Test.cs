using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;

public class IMU_Controller_Test : MonoBehaviour
{

    [SerializeField] private GameObject IMU;

    [SerializeField] private Vector3 noiseRotation = new(0.063f, 0.003f, 0.044f);
    [SerializeField] private Vector3 noiseGravity = new(0, -11.025f, 0);
    [SerializeField] private bool stopAcceleration = false;

    [SerializeField] private Camera camera;

    private float gravitiy = 10.25f;

    private float[] logFloats = new float[600];
    private int i = 0;

    private Vector3 lastAccel;
    private Vector3 lastGyro;
    private Vector3 lastConstSpeed;

    private Vector3 ConstantAccelerationToSpeed(float t, Vector3 acceleration, Vector3 initSpeed)
    {
        return acceleration * t + initSpeed; // - gravity speed
    }

    private Vector3 MeanAccelerationToSpeed(float t, Vector3 acceleration, Vector3 initSpeed) //why does this not works like it should?
    {
        Vector3 v = 1.0f / 2.0f * acceleration * Mathf.Pow(t, 2.0f) + initSpeed * t;
        Debug.LogFormat("x: {0}, y: {1}, z: {2},", v.x, v.y, v.z);
        return v;
    }



    //todo merge events
    // Start is called before the first frame update
    void Start()
    {
        PortDataAccessor pda = PortDataAccessor.Instance;
        if (pda != null)
        {
            pda.Baudrate = 115200;
            pda.PortName = "COM5";

            pda.EventDataHook.registerDataHook("Acceleration_XYZ", (object sender, DataArrivedEventArgs args) =>
            {
                //Debug.Log(timeBuffer);
                /*
                if(i < logFloats.Length) { logFloats[i++] = (timeBuffer * 1000); timeBuffer = 0;  }
                else { 
                    Debug.LogError(Mean(logFloats)); 
                }*/

                // TODO check mapping of Accel to unity koords
                if (lastFrame != null)
                {
                    Vector3 rawAccel = ParseVector3Acceleration(args.Value, ' ');
                    float rawMagnitude = rawAccel.magnitude; // can be uses to distinct between moving and not moving the IMU -> reduce drift by cutoff Band-pass-filter
                    if(rawMagnitude > 9.2f && rawMagnitude < 11.0f)
                    {
                        rawAccel = Vector3.zero;
                    }
                    Vector3 acceleration = LowPassFilter(rawAccel);
                    //Vector3 acceleration = HighPassFilter(rawAccel, timeBuffer);
                    //Vector3 acceleration = rawAccel;
                    lastAccel = acceleration;
                    //Debug.Log(acceleration);
                    Vector3 nextConstSpeed;
                    if (lastConstSpeed != null)
                    {
                        nextConstSpeed = ConstantAccelerationToSpeed(timeBuffer, acceleration, Vector3.zero); //actually its speed
                        Vector3 lastPos = IMU.transform.localPosition;
                        nextConstSpeed.y = 0;
                        IMU.transform.localPosition += nextConstSpeed;
                        //Debug.Log(Vector3.Distance(lastPos, IMU.transform.localPosition) / timeBuffer); // just to check if distance between two transform is the gravitation
                        //camera.transform.localPosition += new Vector3(0.0f, nextConstSpeed.y, 0.0f);

                        //Debug.Log(ComplementaryFilter(timeBuffer));
                    }
                    else
                    {
                        nextConstSpeed = ConstantAccelerationToSpeed(timeBuffer, acceleration, Vector3.zero);
                    }
                    lastConstSpeed = nextConstSpeed;
                }
                else
                {
                    lastFrame = ParseVector3Acceleration(args.Value, ' ');
                }

                timeBuffer = 0;
            });

            pda.EventDataHook.registerDataHook("Rotation_XYZ", (object sender, DataArrivedEventArgs args) =>
            {
                Vector3 rr = ParseVector3Rotation(args.Value, ' ');
                //don't knwo which works better this below or -> rr.sqrMagnitude > 0.058
                if (Mathf.Abs(rr.x) < noiseRotation.x)
                {
                    rr.x = 0;
                }
                if (Mathf.Abs(rr.y) < noiseRotation.y)
                {
                    rr.y = 0;
                }
                if (Mathf.Abs(rr.z) < noiseRotation.z)
                {
                    rr.z = 0;
                }
                lastGyro = rr;
                IMU.transform.Rotate(rr);

            });

            pda.ConnectToPort();
        }
    }

    private float timeBuffer = 0;

    private void Update()
    {
        timeBuffer += Time.deltaTime; // to get the real time between objects
    }

    private Vector3 lastFrame;
    private float bias = 0.8f;
    private Vector3 LowPassFilter(Vector3 nextFrame)
    {
        Vector3 smoothedVector = lastFrame * bias + nextFrame * (1 - bias);
        lastFrame = smoothedVector;
        return smoothedVector;
    }

    float cutoffFreq = 0.8f;
    private Vector3 HighPassFilter(Vector3 nextFrame, float timebuffer)
    {
        float alpha = cutoffFreq / (cutoffFreq + timebuffer);
        Vector3 filteredVector = new Vector3();
        filteredVector.x = lastFrame.x + alpha * (nextFrame.x - lastFrame.x);
        filteredVector.y = lastFrame.y + alpha * (nextFrame.y - lastFrame.y);
        filteredVector.z = lastFrame.z + alpha * (nextFrame.z - lastFrame.z);
        lastFrame = filteredVector;
        return filteredVector;
    }

    Vector3 lastCompValue = Vector3.zero;
    private Vector3 ComplementaryFilter(float dt)
    {
        float bias = 0.98f;
        Vector3 accl = lastAccel;
        float norm = accl.magnitude;



        // As we only can cover half (PI rad) of the full spectrum (2*PI rad) we multiply
        // the unit vector with values from [-1, 1] with PI/2, covering [-PI/2, PI/2].
        float scale = Mathf.PI / 2.0f;

        lastCompValue.z = lastCompValue.z + lastGyro.z * dt;
        lastCompValue.x = (float)(bias * (lastCompValue.x + lastGyro.x * dt) + (1.0 - bias) * (accl.x * scale / norm));
        lastCompValue.y = (float)(bias * (lastCompValue.y + lastGyro.y * dt) + (1.0 - bias) * (accl.y * -scale / norm));

        return lastCompValue;
    }

    private float Mean(float[] f)
    {
        float sum = 0;
        foreach (var item in f)
        {
            sum += item;
        }
        return sum / f.Length;
    }

    private Vector3 ParseVector3Rotation(string s, char delimeter)
    {
        string[] splitValues = s.Split(delimeter);
        return new Vector3(
            float.Parse(splitValues[0], CultureInfo.InvariantCulture),
            -1.0f * float.Parse(splitValues[2], CultureInfo.InvariantCulture), //note y and z is switched
            float.Parse(splitValues[1], CultureInfo.InvariantCulture)
            );
    }

    private Vector3 ParseVector3Acceleration(string s, char delimeter)
    {
        string[] splitValues = s.Split(delimeter);
        return new Vector3(
            float.Parse(splitValues[0], CultureInfo.InvariantCulture),
            -1.0f * float.Parse(splitValues[2], CultureInfo.InvariantCulture), //note y and z is switched
            float.Parse(splitValues[1], CultureInfo.InvariantCulture)
            );
    }
}

