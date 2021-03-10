using System.Collections;
using UnityEngine;
using C = Const.Constants;

public class BlackoutDispenser : Dispenser {
	private readonly float maxTime;

	public float Maxtime {
		get { return maxTime; }
	}

	public BlackoutDispenser(string dispenserName, float maxTime) : base(dispenserName) {
		this.maxTime = maxTime;
	}

	public override void Dispense(string callingGameObjectName = null) {
		float duration = Random.value * maxTime * C.MillisecondToSecond;
		ToggleLights();
		StartCoroutine(Delay(duration));
		ToggleLights();
	}

	public void ToggleLights() {
		Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];
		foreach (Light light in lights) {
			if (light.isActiveAndEnabled)
				light.enabled = false;
			else
				light.enabled = true;
		}
	}
}
