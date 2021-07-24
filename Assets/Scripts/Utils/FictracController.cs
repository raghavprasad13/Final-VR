using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Const;
using System.Text;
using System.IO;
using UnityEngine;

namespace Utils {
    /// <summary>
	/// Contains fields and methods to interact with Fictrac process and stream data from it
	/// Fictrac data header: https://github.com/rjdmoore/fictrac/blob/master/doc/data_header.txt
	/// </summary>
    public class FictracController {
        public static Process fictracProcess = null;

        public static Socket sender;

        public static float frameNumber;
        public static float deltaForward;
        public static float deltaSide;
        public static float deltaRotationY;
        public static int ballDecoupleToggle;   // default value is 1, changes to 0 when the ball is decoupled
        public static int movementInversionToggle;  // default value is 1, changes to -1 when the moevement inversion occurs
        public static bool isReplay = false;

        public static string replayCsvFileName = null;

        private static string data = "";

        // For Windows
        public static void StartFictrac() {
            ballDecoupleToggle = 1;
            movementInversionToggle = 1;

            fictracProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(Application.streamingAssetsPath, "fictrac_starter.bat"),
					//FileName = "C:\\Users\\RatTracker1\\fictrac_starter.bat",
					UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            fictracProcess.Start();
        }

        // For Mac/Linux
        public static void StartFictrac(string command) {
            ballDecoupleToggle = 1;
            movementInversionToggle = 1;

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

        public static void ReceiveData() {
            byte[] messageReceived = new byte[1024];
            int byteReceived = sender.Receive(messageReceived);
            string newData = Encoding.ASCII.GetString(messageReceived, 0, byteReceived);

            data += newData;

            int endline = data.IndexOf('\n');
            string line = data.Substring(0, endline);
            data = data.Substring(endline + 1);

            string[] delim = { ", " };
            string[] tokens = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            frameNumber = float.Parse(tokens[1]);
            deltaForward = float.Parse(tokens[20]);
            deltaSide = float.Parse(tokens[21]);
            deltaRotationY = float.Parse(tokens[7]);
        }
    }
}
