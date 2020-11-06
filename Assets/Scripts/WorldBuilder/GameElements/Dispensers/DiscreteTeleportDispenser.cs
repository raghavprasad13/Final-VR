using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteTeleportDispenser : Dispenser {
	bool sequential;
	float delay;
	List<Tuple<Vector3, Vector2>> destinations;

	public bool Sequential {
		get { return sequential; }
	}

	public float Delay {
		get { return delay; }
	}

	public List<Tuple<Vector3, Vector2>> Destinations {
		get { return destinations; }
	}

	public DiscreteTeleportDispenser(string dispenserName, bool sequential, float delay, List<Tuple<Vector3, Vector2>> destinations) : base(dispenserName) {
		this.sequential = sequential;
		this.delay = delay;
		this.destinations = destinations;
	}

	public override void Dispense() {
		GameObject avatar = GameObject.Find("Avatar");
		avatar.transform.position = destinations[0].Item1;
		// TODO: Add code to set destinationOrientation
		ExecuteTriggers();
	}

	public void ExecuteTriggers() {

	}
}
