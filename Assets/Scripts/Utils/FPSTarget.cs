using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes sure the VR application runs at the same frame rate as the Fictrac camera
/// </summary>
public class FPSTarget : MonoBehaviour {
	public int TARGET = Data.TargetFps;

	private void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = TARGET;
	}

	void Update() {
		if (Application.targetFrameRate != TARGET)
			Application.targetFrameRate = TARGET;
    }
}
