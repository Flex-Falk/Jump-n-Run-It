using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHandler
{
    public static Boolean JumpInput()
    {
        return Input.GetButtonDown("Jump") 
            || (UdpSocket.Instance.lastPrediction == Prediction.Neutral 
                && UdpSocket.Instance.curPrediction == Prediction.Jump_Forward);
    }
    public static Boolean PlayerRunInput()
    {
        return (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"));
    }
    //returns the LaneNumber, the Player
    public static float LaneInput()
    {
        float lane = Input.GetAxisRaw("Horizontal");
        float middleLane = 1;
        if (lane == -1)
        {
            return 0;
        }
        else if (lane == 1)
        {
            return 2;
        }
        return middleLane;


    }
    public static Boolean RunInput()
    {
        return false;
    }
    public static bool AttackInput()
    {
        return Input.GetButtonDown("Attack")
            || (UdpSocket.Instance.lastPrediction == Prediction.Neutral 
                    && UdpSocket.Instance.curPrediction == Prediction.Shoot_Forward);
    }

    public static bool CrouchInput()
    {
        return Input.GetButtonDown("Crouch")
            || (UdpSocket.Instance.lastPrediction == Prediction.Neutral 
                    && UdpSocket.Instance.curPrediction == Prediction.Crouch_Forward);
    }

}
