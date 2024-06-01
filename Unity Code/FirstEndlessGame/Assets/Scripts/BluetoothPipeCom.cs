using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Pipes;


public class BluetoothPipeCom : UnityEngine.MonoBehaviour
{

    private static string PIPE_NAME = "Bluetooth_Pipe";

    // Start is called before the first frame update
    // TODO: convert to async 
    void Start()
    {
        using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream(".", PIPE_NAME, PipeDirection.In))
        {

            // Connect to the pipe or wait until the pipe is available.
            UnityEngine.Debug.LogFormat("Attempting to connect to pipe...");
            pipeClient.Connect();

            UnityEngine.Debug.LogFormat("Connected to pipe.");
            UnityEngine.Debug.LogFormat("There are currently {0} pipe server instances open.",
               pipeClient.NumberOfServerInstances);
            using (StreamReader sr = new StreamReader(pipeClient))
            {
                // Display the read text to the console
                string temp;
                while ((temp = sr.ReadLine()) != null) 
                {
                    UnityEngine.Debug.LogFormat("Received from server: {0}", temp);
                }
            }
        }
        UnityEngine.Debug.LogFormat("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
