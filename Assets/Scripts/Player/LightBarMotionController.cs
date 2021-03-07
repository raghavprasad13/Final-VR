using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBarMotionController : MonoBehaviour {
    LightBar lightBar;
    float radius, angle, angularFrequency;
    // Start is called before the first frame update
    void Start() {
        lightBar = TrackFileParser.track.LightBar;
        radius = lightBar.Center.magnitude;
        print("LIGHTBAR RADIUS: " + radius);
        angularFrequency = 1 / lightBar.TimePeriod;
    }

    // Update is called once per frame
    void Update() {
        angle += (angularFrequency / radius) * Time.deltaTime;
        transform.position = new Vector3(Mathf.Cos(angle), lightBar.Height, Mathf.Sin(angle)) * radius;
    }
}
