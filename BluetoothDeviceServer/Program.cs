using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace BluetoothDeviceServer
{
    class PipeServer
    {
        private static string PIPE_NAME = "Bluetooth_Pipe";

        private NamedPipeServerStream pipeServer;

        private Task taskReadIn;
        public bool TerminatReading { get; set; }

        private Dictionary<PipeDirection, List<Action<string>>> registeredInOutEvents = new Dictionary<PipeDirection, List<Action<string>>>();
        private List<Action> registeredOnNewConnectionEvents = new List<Action>();

        private Object token = new Object();
        private IAsyncResult asyncResultOfWaitForConnection;
        public PipeServer(string[] args)
        {
            TerminatReading = false;
            pipeServer = new NamedPipeServerStream(PIPE_NAME, PipeDirection.InOut);
            Console.WriteLine(string.Format("NamedPipeServerStream {0} object created.", PIPE_NAME));

            registeredInOutEvents[PipeDirection.In] = new List<Action<string>>();
            registeredInOutEvents[PipeDirection.Out] = new List<Action<string>>();
        }

        public void WaitForConnectionSync()
        {
            pipeServer.WaitForConnection();
            Console.WriteLine("Client Connected to this server");
            registeredOnNewConnectionEvents.ForEach((Action callback) => { callback.Invoke(); });
        }

        public void RunPipeInThread()
        {
            taskReadIn = new Task(() =>
            {
                TerminatReading = false;
                while (TerminatReading)
                {
                    string strFromClient = ReadPipeIn();
                    if (strFromClient != null)
                    {
                        registeredInOutEvents[PipeDirection.In].ForEach((Action<string> callback) => { callback.Invoke(strFromClient); });

                    }
                    Thread.Sleep(15);
                }
            });
        }

        // Get data from connected clients to this server
        private string ReadPipeIn()
        {
            try
            {
                StreamReader sw = new StreamReader(pipeServer);
                if (sw.EndOfStream == false)
                {
                    return sw.ReadLine();
                }
                else
                {
                    return null;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            return null;
        }

        // Send data from this server to the connected clients
        public void WritePipeOut(string outString)
        {
            try
            {
                StreamWriter sw = new StreamWriter(pipeServer);
                sw.AutoFlush = true;
                sw.WriteLine(outString);
                registeredInOutEvents[PipeDirection.Out].ForEach((Action<string> callback) => { callback.Invoke(outString); });
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
        }

        public void AddEventListener(PipeDirection direction, Action<string> callback)
        {
            registeredInOutEvents[direction].Add(callback);
        }

        public bool RemoveEventListener(PipeDirection direction, Action<string> callback)
        {
            return registeredInOutEvents[direction].Remove(callback);
        }


        public void AddEventlistenerOnNewConnection(Action callback)
        {
            registeredOnNewConnectionEvents.Add(callback);
        }

        public bool RemoveEventlistenerOnNewConnection(Action callback)
        {
            return registeredOnNewConnectionEvents.Remove(callback);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            PipeServer pipeServer = new PipeServer(args);
            pipeServer.AddEventListener(PipeDirection.Out, (string s) => { Console.WriteLine(s); });
            pipeServer.AddEventlistenerOnNewConnection(() => { Console.WriteLine("New Connection Invoke"); });

            pipeServer.WaitForConnectionSync();
            pipeServer.RunPipeInThread();
            pipeServer.WritePipeOut("This is a Message from a Server to all Clients");

            Console.WriteLine("Input anything to close this pipe server!");
            string stop = Console.ReadLine();
            pipeServer.TerminatReading = true;
        }
    }
}
