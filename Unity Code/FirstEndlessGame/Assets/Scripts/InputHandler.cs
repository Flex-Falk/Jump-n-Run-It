using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHandler
{
    public static Boolean JumpInput()
    {
        return Input.GetButton("Jump");
    }
    public static Boolean ChangeDirectionInput()
    {
        return Input.GetButtonDown("Horizontal");
    }
    public static float DirectionInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public static Boolean RunInput() 
    { 
        return false;
    }
    public static bool AttackInput() 
    {
        return Input.GetButton("Fire1");
    }

    public static bool CrouchInput() 
    {
        return false;
    }
  
}
