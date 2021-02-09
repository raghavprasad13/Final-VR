using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
	private const float ROTATION_GAIN = 50f;
	private const float TRANSLATION_GAIN = 10f;
	private const float INVERT = -1f;

	private void Update() {
		float forward = Input.GetAxis("Vertical") * TRANSLATION_GAIN * Time.deltaTime * INVERT;
		var rotation = Input.GetAxis("Horizontal") * ROTATION_GAIN * Time.deltaTime * INVERT;
		transform.Rotate(forward, rotation, 0, Space.World);
	}
}
