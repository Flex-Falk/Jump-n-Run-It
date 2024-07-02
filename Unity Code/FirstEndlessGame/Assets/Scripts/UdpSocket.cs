using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public enum Prediction
{
    Neutral,
    Jump_Forward,
    Jump_Backward,
    Shoot_Forward,
    Shoot_Backward,
    Crouch_Forward,
    Crouch_Backward
}

public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    [SerializeField] int rxPort = 8000; // port to receive data from Python on
    [SerializeField] int txPort = 8001; // port to send data to Python on

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    public static UdpSocket Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // to prevent copys after the first instance of this class
        }
        else
        {
            Instance = this;
            // Create remote endpoint (to Matlab) 
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

            // Create local client
            client = new UdpClient(rxPort);

            // local endpoint define (where messages are received)
            // Create a new thread for reception of incoming messages
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();

            // Initialize (seen in comments window)
            Debug.Log("[UDP] " + "UDP Comms Initialised");
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] string msgToServer = "";
    [ContextMenu("SendDataFromEditor")]
    public void SendDataFromEditor() // Use to send data to Python
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(msgToServer);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    public void SendData(string message) // Use to send data to Python
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }




    public Prediction lastPrediction = Prediction.Neutral;
    public Prediction curPrediction = Prediction.Neutral;

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                if (text.Equals("Neutral"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Neutral;
                }
                else if (text.Equals("Jump_Forward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Jump_Forward;
                }
                else if (text.Equals("Jump_Backward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Jump_Backward;
                }
                else if (text.Equals("Shoot_Forward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Shoot_Forward;
                }
                else if (text.Equals("Shoot_Forward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Shoot_Backward;
                }
                else if (text.Equals("Crouch_Forward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Crouch_Forward;
                }
                else if (text.Equals("Crouch_Backward"))
                {
                    lastPrediction = curPrediction;
                    curPrediction = Prediction.Crouch_Backward;
                }

                Debug.Log("[Prediction] " + text);
                ProcessInput(text);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessInput(string input)
    {
        // PROCESS INPUT RECEIVED STRING HERE

        if (!isTxStarted) // First data arrived so tx started
        {
            isTxStarted = true;
        }
    }

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
        }

        client.Close();
    }
}
