using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Const;
using System.Text;

namespace Utils {
    public class FictracController {
        public static Process fictracProcess = null;

        public static Socket sender;

        // For Windows
        public static void StartFictrac() {
            fictracProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "C:\\Users\\RatTracker1\\fictrac_starter.bat",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            fictracProcess.Start();
        }

        // For Mac/Linux
        public static void StartFictrac(string command) {
            command = command.Replace("\"", "\"\"");

            fictracProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            fictracProcess.Start();
        }

        public static void StopFictrac() {
            if (fictracProcess == null || fictracProcess.HasExited)
                return;

            try {
                sender.Shutdown(SocketShutdown.Both);
            } finally {
                sender.Close();
            }

            fictracProcess.Kill();
        }

        /// <summary>
		/// Fictrac socket connection code
		/// </summary>
        public static void FictracClient() {
            IPAddress ipAddr = IPAddress.Parse(Constants.LOCALHOST);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Constants.FictracPort);

            sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(localEndPoint);

            //print("Socket connected to -> " + sender.RemoteEndPoint.ToString());
        }

        public static string ReceiveData() {
            byte[] messageReceived = new byte[1024];
            int byteReceived = sender.Receive(messageReceived);
            string newData = Encoding.ASCII.GetString(messageReceived, 0, byteReceived);

            return newData;
        }
    }
}
