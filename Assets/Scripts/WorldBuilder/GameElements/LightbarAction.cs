using UnityEngine;

public class LightbarAction : MonoBehaviour {
	public LightBar lightBar;

	private void Start() {
		lightBar = TrackFileParser.track.LightBar;
	}

	private void OnTriggerEnter(Collider other) {
		print("TRIGGERED!");
		lightBar.RewardTrigger.ExecuteTrigger(gameObject.name);
	}
}
