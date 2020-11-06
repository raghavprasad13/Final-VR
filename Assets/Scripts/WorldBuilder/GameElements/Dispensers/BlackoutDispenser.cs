using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutDispenser : Dispenser {
	float maxTime;

	public float Maxtime {
		get { return maxTime; }
	}

	public BlackoutDispenser(string dispenserName, float maxTime) : base(dispenserName) {
		this.maxTime = maxTime;
	}

	public override void Dispense() {
		float duration = Random.value * maxTime;
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

	IEnumerator Delay(float delayTime) {
		yield return new WaitForSeconds(delayTime);
	}
}
