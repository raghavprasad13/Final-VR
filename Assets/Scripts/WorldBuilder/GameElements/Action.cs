using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
	bool hasEntered = false;

	public static Dispenser dispenser;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Avatar"))
			hasEntered = true;
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Avatar"))
			hasEntered = false;
	}

	private void Update() {
		if(hasEntered && dispenser != null) {
			//dispenser.Dispense();
			print("Inside!");
		}
	}
}
