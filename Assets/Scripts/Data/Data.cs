using System;
using System.IO;
using UnityEngine;

public class Data {
	public static string LogFile;
    public static long ExperimentStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    public static string outputFilesDir = Path.Combine("Assets", "OutputFiles~");

    public static void LogHeaders() {
        using (var writer = new StreamWriter(outputFilesDir + LogFile, false)) {
            writer.Write(
                "Timestamp,TimeSinceStart,PositionX,PositionY,PositionZ,RotationY," +
                "EventOccurred,EventX,EventZ,EventDescription,UpArrow,DownArrow," +
                " LeftArrow,RightArrow,Space"
            + "\n");
            writer.Flush();
            writer.Close();
        }
    }

    public static void LogData(Transform t, int eventOccurred = 0, string eventTag = null, string eventDescription = "null") {
        if (!Directory.Exists(outputFilesDir))
            Directory.CreateDirectory(outputFilesDir);

        using (var writer = new StreamWriter(outputFilesDir + LogFile, true)) {
            var PositionX = t.position.x.ToString();
            var PositionZ = t.position.z.ToString();
            var PositionY = t.position.y.ToString();
            var RotationY = t.eulerAngles.y.ToString();

            var eventX = float.NaN.ToString();
            var eventZ = float.NaN.ToString();


            if (eventTag != null) {
                Transform eventTransform = GameObject.Find(eventTag).transform;
                eventX = eventTransform.position.x.ToString();
                eventZ = eventTransform.position.z.ToString();
            }

            var timeSinceExperimentStart = DateTimeOffset.Now.ToUnixTimeMilliseconds() - ExperimentStartTime;

            var str = string.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7}," +
                "{8},{9},{10},{11},{12},{13},{14}",
                DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), timeSinceExperimentStart,
                PositionX, PositionY, PositionZ, RotationY,
                eventOccurred, eventX, eventZ, eventDescription.ToString(),
                Input.GetKey(KeyCode.UpArrow) ? 1 : 0,
                Input.GetKey(KeyCode.DownArrow) ? 1 : 0, Input.GetKey(KeyCode.LeftArrow) ? 1 : 0,
                Input.GetKey(KeyCode.RightArrow) ? 1 : 0,
                Input.GetKey(KeyCode.Space) ? 1 : 0);
            writer.Write(str + "\n");
            writer.Flush();
            writer.Close();
        }
    }

    public static void WriteSum(string text, string fileName) {
        using (var writer = new StreamWriter(outputFilesDir + fileName, false)) {
            writer.Write(text + "\n");
            writer.Flush();
            writer.Close();
        }
    }
}
