using System.Collections;
using System.Collections.Generic;
using System;
using System.IO.Ports;
using UnityEngine;

public class DataArrivedEventArgs : EventArgs
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class EventDataHook
{
    public SerialPort Port { get; set; }
    public EventDataHook(SerialPort serialPort)
    {
        Port = serialPort;
    }

    private HashSet<string> registerdKeys = new HashSet<string>();
    private Dictionary<string, EventHandler<DataArrivedEventArgs>> registerdEvents = new();

    //private int BUFFERSIZE = 4096;
    private char[] _internalBuffer = new char[4096];
    private string contents;

    public void registerDataHook(string key, EventHandler<DataArrivedEventArgs> callback)
    {
        if (registerdKeys.Contains(key))
        {
            registerdEvents[key] += callback;
        }
        else
        {
            registerdKeys.Add(key);
            registerdEvents.Add(key, callback);
        }
    }

    public void unregisterDataHook(string key, EventHandler<DataArrivedEventArgs> callback)
    {
        if (registerdKeys.Contains(key))
        {
            registerdEvents[key] -= callback;
        }
        else
        {
            Debug.LogErrorFormat("Key: [{0}] does not exists.", key);
        }
    }

    public void CollectFromPort()
    {
        if (Port != null && Port.IsOpen && Port.BytesToRead > 0)
        {
            int bufferOffset = Port.Read(_internalBuffer, 0, Port.BytesToRead);

            contents += new String(_internalBuffer, 0, bufferOffset);

            foreach (KeyValuePair<string, string> pair in NextPair())
            {
                if (registerdKeys.Contains(pair.Key))
                {
                    //args
                    DataArrivedEventArgs eventArgs = new DataArrivedEventArgs();
                    eventArgs.Key = pair.Key;
                    eventArgs.Value = pair.Value;

                    // call all attached handler on this event for a given key
                    OnArrivedData(registerdEvents[pair.Key], eventArgs);
                }
                else
                {
                    Debug.Log(String.Format("Not registered Key: {0}", pair.Key));
                }
            }
        }
    }

    private IEnumerable<KeyValuePair<string, string>> NextPair()
    {
        int kvIndex = contents.IndexOf(';'); //late check for escaping
        while (kvIndex > -1)
        {
            string kv = contents.Substring(0, kvIndex);
            int kIndex = kv.IndexOf(':');
            yield return new KeyValuePair<string, string>(kv.Substring(0, kIndex), kv.Substring(kIndex + 1));
            contents = contents.Substring(kvIndex + 1); //remove added kv-pair
            kvIndex = contents.IndexOf(';'); // search or next pair
        }

        yield break;
    }

    protected virtual void OnArrivedData(EventHandler<DataArrivedEventArgs> e, DataArrivedEventArgs eventArgs)
    {
        if (e != null)
        {
            e(this, eventArgs);
        }
    }
}
