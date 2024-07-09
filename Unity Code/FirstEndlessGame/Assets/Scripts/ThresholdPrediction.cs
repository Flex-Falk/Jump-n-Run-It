using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public struct StddevIMU
{
    public StddevIMU(float _x, float _z, float _yaw, float _pitch, float _roll)
    {
        x = _x;
        z = _z;
        yaw = _yaw;
        pitch = _pitch;
        roll = _roll;

    }
    public float x { get; }
    public float z { get; }
    public float yaw { get; }
    public float pitch { get; }
    public float roll { get; }
}


public class ThresholdPrediction
{
    StddevIMU stddevIMU = new StddevIMU(
        1.392059e+08f,
        6.689037e+06f,
        2.022568e+01f,
        4.468770e+00f,
        1.768963e+01f
        );

    public float[] CalcGradient(float[] latestPoint, float[] currentPoint)
    {
        float[] gradient = new float[latestPoint.Length];

        for (int i = 0; i < gradient.Length; ++i)
        {
            float dif = currentPoint[i] - latestPoint[i];
            gradient[i] = dif/Mathf.Abs(dif) * Mathf.Pow(dif, 2);
        }

        return gradient;
    }

    private bool PredictJumpForward(float z, float yaw, float pitch, float roll)
    {
        return
            stddevIMU.z < z
            && -stddevIMU.yaw > yaw
            && stddevIMU.pitch > Mathf.Abs(pitch)
            && -stddevIMU.roll > roll
            && roll > -200;
    }

    private bool PredictJumpBackward(float z, float yaw, float pitch, float roll)
    {
        return
            -stddevIMU.z > z
            && stddevIMU.yaw < yaw
            && stddevIMU.pitch > Mathf.Abs(pitch)
            && stddevIMU.roll < roll
            && roll < 200;
    }

    private bool PredictShootForward(float z, float yaw, float pitch, float roll)
    {
        return stddevIMU.yaw < yaw
            && -stddevIMU.pitch > pitch
            && -200 > roll;
    }

    private bool PredictShootBackward(float z, float yaw, float pitch, float roll)
    {
        return -stddevIMU.z > z
            && -stddevIMU.yaw > yaw
            && stddevIMU.pitch < pitch
            && roll > 150;
    }

    private bool PredictCrouchForward(float z, float yaw, float pitch, float roll)
    {
        return -stddevIMU.yaw > yaw
            && stddevIMU.pitch > Mathf.Abs(pitch)
            && stddevIMU.roll < roll
            && roll < 150;
    }

    private bool PredictCrouchBackward(float z, float yaw, float pitch, float roll)
    {
        return stddevIMU.yaw < yaw
            && stddevIMU.pitch > Mathf.Abs(pitch)
            && -stddevIMU.roll > roll
            && roll > -150;
    }

    Prediction latestPrediction = Prediction.Neutral;

    public Prediction Predict(float[] gradient)
    {
        float z = gradient[0];
        float yaw = gradient[1];
        float pitch = gradient[2];
        float roll = gradient[3];

        if (PredictShootForward(z, yaw, pitch, roll))
        {
            latestPrediction = Prediction.Shoot_Forward;
            return Prediction.Shoot_Forward;
        }
        else if (PredictShootBackward(z, yaw, pitch, roll) && latestPrediction == Prediction.Shoot_Forward)
        {
            return Prediction.Shoot_Backward;
        }
        else if (PredictJumpForward(z, yaw, pitch, roll))
        {
            latestPrediction = Prediction.Jump_Forward;
            return Prediction.Jump_Forward;
        }
        else if (PredictJumpBackward(z, yaw, pitch, roll) && latestPrediction == Prediction.Jump_Forward)
        {
            return Prediction.Jump_Backward;
        }
        else if (PredictCrouchForward(z, yaw, pitch, roll))
        {
            latestPrediction = Prediction.Crouch_Forward;
            return Prediction.Crouch_Forward;
        }
        else if (PredictCrouchBackward(z, yaw, pitch, roll) && latestPrediction == Prediction.Crouch_Forward)
        {
            return Prediction.Crouch_Backward;
        } else
        {
            return Prediction.Neutral;
        }
    }

    public void Test()
    {
        float[] jump_f = new float[] { 8.5e+07f, -60, 4, -95};
        float[] jump_b = new float[] { -1.11e+08f, 50.1f, 0, 120.2f};
        Debug.Log("Expected: Jump_Forward: " + Predict(jump_f));
        Debug.Log("Expected: Jump_Backward: " + Predict(jump_b));

        float[] shoot_f = new float[] { -4.3e+07f, 199, -1.734e+04f, -216.1f};
        float[] shoot_b = new float[] { -3.0e+07f, -130.7f, 4.82e+03f, 168.4f };
        Debug.Log("Expected: Shoot_Forward: " + Predict(shoot_f));
        Debug.Log("Expected: Shoot_Forward: " + Predict(shoot_b));

        float[] crouch_f = new float[] { 0, -379, 0, 46};
        float[] crouch_b = new float[] { 0, 72, 0, -44.7f};
        Debug.Log("Expected: Crouch_Forward: " + Predict(crouch_f));
        Debug.Log("Expected: Crouch_Forward: " + Predict(crouch_b));
    }

}
