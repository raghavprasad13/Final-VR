using UnityEngine;

public class HorizontalBarMotionController : MonoBehaviour {
    LightBar lightBar;
    float frequency, radius, angle = 0f;
    int direction;

    void Start() {
        lightBar = TrackFileParser.track.LightBar;
        radius = lightBar.Center.magnitude;
        frequency = 1 / lightBar.TimePeriod;
        direction = lightBar.IsDirectionPositive ? 1 : -1;
    }

    void Update() {
        angle += 2 * Mathf.PI * frequency * Time.deltaTime;     // angle in radians
        transform.position = direction * radius * new Vector3(Mathf.Cos(angle), lightBar.Height, Mathf.Sin(angle));
    }
}
