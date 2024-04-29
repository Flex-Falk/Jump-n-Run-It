using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class PortDataAccessor : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int baudrate = 115200;
    [SerializeField] string portName = "COM7";
    public SerialPort Port { get; private set; }
    public EventDataHook EventDataHook { get; private set; }

    public static PortDataAccessor Instance { get; private set; }

    // Todo: check available ports and auto set ESP

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            Instance = this;
            //initcode here
            try
            {
                Port = new SerialPort();
                Port.BaudRate = baudrate;
                Port.PortName = portName;
                Port.Open();
                Debug.Log("Port Open");
                EventDataHook = new(Port);

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        EventDataHook.CollectFromPort();
    }

    private void OnDestroy()
    {
        EventDataHook.Port = null;
        Port.Close();
    }

    public void SendToPort(string data)
    {
        Port.Write(data);
    }

}
