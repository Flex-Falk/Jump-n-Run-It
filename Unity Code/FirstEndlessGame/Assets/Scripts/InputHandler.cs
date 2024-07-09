using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movements
{
    Neutral,
    Jump,
    Crouch,
    Shoot
}
public static class InputHandler
{
    /*
    public static Movements lastPredicted = Movements.Neutral;
    public static Movements currentPredicted = Movements.Neutral;
    */
    public static Prediction lastPredicted = Prediction.Neutral;
    public static Prediction currentPredicted = Prediction.Neutral;
    public static Boolean JumpInput()
    {
        return currentPredicted == Prediction.Jump_Backward;
            /*
            (UdpSocket.Instance.lastPrediction == Prediction.Neutral
                && UdpSocket.Instance.curPrediction == Prediction.Jump_Forward)
            || (UdpSocket.Instance.lastPrediction == Prediction.Neutral
                && UdpSocket.Instance.curPrediction == Prediction.Jump_Backward);
            Input.GetButtonDown("Jump") 
            || currentPredicted == Movements.Jump;
            */
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
        return currentPredicted == Prediction.Shoot_Backward;
        
        /*
        (UdpSocket.Instance.lastPrediction == Prediction.Neutral 
            && UdpSocket.Instance.curPrediction == Prediction.Shoot_Forward);
        Input.GetButtonDown("Attack")
        || currentPredicted == Movements.Shoot;
        */
    }

    public static bool CrouchInput()
    {
        return currentPredicted == Prediction.Crouch_Backward;
            /*
            (UdpSocket.Instance.lastPrediction == Prediction.Neutral 
                && UdpSocket.Instance.curPrediction == Prediction.Crouch_Forward);
            Input.GetButtonDown("Crouch")
            || currentPredicted == Movements.Crouch;
            */
    }

}
