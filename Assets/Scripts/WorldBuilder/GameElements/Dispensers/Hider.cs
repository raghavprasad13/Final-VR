using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Dispenser {

	List<string> targets;

	public List<string> Targets {
		get { return targets; }
	}

	public Hider(string dispenserName, List<string> targets) : base(dispenserName) {
		this.targets = targets;
	}

	public override void Dispense(string callingGameObjectName = null) {
		GameObject targetGameObject;
		foreach(string target in targets) {
			targetGameObject = GameObject.Find(target);
			//bool currentStatus = targetGameObject.GetComponent<Renderer>().enabled;
			//targetGameObject.GetComponent<Renderer>().enabled = !currentStatus;     // This would merely make it invisible, it would otherwise be active in the scene

			bool currentStatus = targetGameObject.activeSelf;
			targetGameObject.SetActive(!currentStatus); // This would be the equivalent of unchecking the tick mark in the Unity editor
		}
	}

	/// <summary>
	/// This Undispense method is specific to Hiders since then would also need to unhide at some point
	/// </summary>
	public void Undispense() {
		GameObject targetGameObject;
		foreach (string target in targets) {
			targetGameObject = GameObject.Find(target);
			targetGameObject.GetComponent<Renderer>().enabled = true;  // This would make it visible, it would otherwise be active in the scene
			//targetGameObject.SetActive(true);	// This would be the equivalent of unchecking the tick mark in the Unity editor
		}
	}
}
