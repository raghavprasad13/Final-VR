using UnityEngine;

public class TorusMotionController : MonoBehaviour {
	LightBar lightBar;
	private float amplitude, frequency, t;
	int direction;

	private void Start() {
		lightBar = TrackFileParser.track.LightBar;
		frequency = 1 / lightBar.TimePeriod;
		amplitude = lightBar.Height;
		direction = lightBar.IsDirectionPositive ? 1 : -1;
	}

	private void Update() {
		t += Time.deltaTime;
		float newY = amplitude * Mathf.Cos(2 * Mathf.PI * frequency * t);

		Vector3 currentPosition = transform.position;
		transform.position = new Vector3(currentPosition.x, newY, currentPosition.z) * direction;
	}
}
