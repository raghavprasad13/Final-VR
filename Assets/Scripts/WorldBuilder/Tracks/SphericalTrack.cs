using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalTrack : Track {
	public float radius;

	public float Radius {
		get { return radius; }
	}

	public SphericalTrack(float radius) : base() {
		this.radius = radius;
	}
}
