﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteTeleportDispenser : Dispenser {
	private readonly bool sequential;
	private readonly float delayTime;
	private readonly List<Tuple<Vector3, Vector2>> destinations;	// A destination is a combination of the Avatar position and orientation. Thus, each destination is a 2-tuple 

	public bool Sequential {
		get { return sequential; }
	}

	public float DelayTime {
		get { return delayTime; }
	}

	public List<Tuple<Vector3, Vector2>> Destinations {
		get { return destinations; }
	}

	public DiscreteTeleportDispenser(string dispenserName, bool sequential, float delayTime, List<Tuple<Vector3, Vector2>> destinations) : base(dispenserName) {
		this.sequential = sequential;
		this.delayTime = delayTime;
		this.destinations = destinations;
	}

	public override void Dispense(string callingGameObjectName = null) {
		/* TODO 
		 * Implement logic for sequential and to use multiple destinations
		 */
		GameObject avatar = GameObject.Find("Avatar");
		avatar.transform.position = destinations[0].Item1;

		Vector2 orientation = destinations[0].Item2;
		if (orientation.y == -1)
			avatar.transform.rotation = Quaternion.LookRotation(Vector3.back);
		else if (orientation.y == 1)
			avatar.transform.rotation = Quaternion.LookRotation(Vector3.forward);

		ExecuteTriggers();
	}
}
