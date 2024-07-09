using UnityEngine;
using System.Collections.Generic;
using System.IO.Ports;
using System;

public class PortDataAccessor : MonoBehaviour
{
    [SerializeField] private int id;

    public int Id { get; private set; }
    public int Baudrate { get; set; }
    public string PortName { get; set; }
    public SerialPort Port { get; private set; }
    public EventDataHook EventDataHook { get; private set; }

    public static PortDataAccessor Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            EventDataHook = new EventDataHook(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EventDataHook != null)
        {
            EventDataHook.CollectFromPort();
        }
    }

    private void OnDestroy()
    {
        if (EventDataHook != null)
        {
            EventDataHook.Port = null;
        }
        if (Port != null)
        {
            CloseConnectionToPort();
        }
    }

    public bool ConnectToPort()
    {
        try
        {
            Port = new SerialPort();
            Port.BaudRate = Baudrate;
            Port.PortName = PortName;
            EventDataHook.Port = Port;
            Port.Open();
            return true;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public bool CloseConnectionToPort()
    {
        try
        {
            Port.Close();
            return true;
        } catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void SendToPort(string data)
    {
        Port.Write(data);
    }
}
