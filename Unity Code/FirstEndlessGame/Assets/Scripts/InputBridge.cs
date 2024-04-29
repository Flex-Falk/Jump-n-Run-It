using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Sends the pressed input keys to the ESP, which will send it right back to the 
 * PortDataAccessor. So we can simulate with the inputs that comes from the ESP.
 * To use this class load "Jump-n-Run-It\ESP32 Code\Test and Emulation\input_relay.ino" on the ESP.
 */
public class InputBridge : MonoBehaviour
{
    PortDataAccessor portDataAccessor;

    // Start is called before the first frame update
    void Start()
    {
        portDataAccessor = PortDataAccessor.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            portDataAccessor.SendToPort("jump:1;");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            portDataAccessor.SendToPort("right:1;");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            portDataAccessor.SendToPort("left:1;");
        }
    }
}
