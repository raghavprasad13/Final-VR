using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data {
	public static string LogFile;
    public static int TargetFps = 55;
    private static float _timer = 0;
    public static long ExperimentStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public static void LogHeaders() {
        using (var writer = new StreamWriter("Assets/OutputFiles~/" + LogFile, false)) {
            writer.Write(
                "Timestamp, PositionX, PositionY, PositionZ, RotationY, " +
                "Collision, GoalX, GoalZ, UpArrow, DownArrow, " +
                " LeftArrow, RightArrow, Space"
            + "\n");
            writer.Flush();
            writer.Close();
        }
    }

    public static void LogData(Transform t, int targetFound = 0, string eventTag = null) {
        if (_timer > 1f / (TargetFps == 0 ? 1000 : TargetFps)) {
            using (var writer = new StreamWriter("Assets/OutputFiles~/" + LogFile, true)) {
                var PositionX = t.position.x.ToString();
                var PositionZ = t.position.z.ToString();
                var PositionY = t.position.y.ToString();
                var RotationY = t.eulerAngles.y.ToString();

                Transform eventTransform = GameObject.FindGameObjectWithTag(eventTag).transform;
                float eventX = eventTransform.position.x;
                float eventZ = eventTransform.position.z;

                var timeSinceExperimentStart = DateTimeOffset.Now.ToUnixTimeMilliseconds() - ExperimentStartTime;

                var str = string.Format(
                    "{0}, {1}, {2}, {3}, {4}, {5}, {6}, " +
                    "{7}, {8}, {9}, {10}, {11}, {12}",
                    DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), PositionX, PositionY, PositionZ, RotationY,
                    targetFound, eventX, eventZ,
                    Input.GetKey(KeyCode.UpArrow) ? 1 : 0,
                    Input.GetKey(KeyCode.DownArrow) ? 1 : 0, Input.GetKey(KeyCode.LeftArrow) ? 1 : 0,
                    Input.GetKey(KeyCode.RightArrow) ? 1 : 0,
                    Input.GetKey(KeyCode.Space) ? 1 : 0);
                writer.Write(str + "\n");
                writer.Flush();
                writer.Close();
            }
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}
