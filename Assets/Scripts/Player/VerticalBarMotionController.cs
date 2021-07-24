using UnityEngine;

public class VerticalBarMotionController : MonoBehaviour {
    LightBar lightBar;
    float radius, angle, frequency, direction;

    void Start() {
        lightBar = TrackFileParser.track.LightBar;
        radius = lightBar.Center.magnitude;
        print("LIGHTBAR RADIUS: " + radius);
        frequency = 1 / lightBar.TimePeriod;
        direction = lightBar.IsDirectionPositive ? 1 : -1;
    }

    void Update() {
        angle += 2 * Mathf.PI * frequency * Time.deltaTime;     // angle in radians
		transform.position = new Vector3(Mathf.Cos(angle), lightBar.Height, Mathf.Sin(angle)) * radius * direction;
	}
}
