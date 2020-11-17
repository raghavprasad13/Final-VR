using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
	bool hasEntered = false;

	public OccupationZone occupationZone;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Avatar")) {
			foreach (Trigger trigger in occupationZone.EntryTriggers)
				trigger.ExecuteTrigger();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Avatar"))
			hasEntered = false;
	}

	private void Update() {
		if(hasEntered && occupationZone != null) {
			//dispenser.Dispense();
			print("Inside!");
		}
	}
}
