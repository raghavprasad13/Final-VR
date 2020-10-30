using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticDistanceTrigger {
	string name;
	float minDistance;
	float maxDistance;
	float timeScale;

	public string Name {
		get { return name; }
	}

	public float MinDistance {
		get { return minDistance; }
	}

	public float MaxDistance {
		get { return maxDistance; }
	}

	public float TimeScale {
		get { return timeScale; }
	}

	public ProbabilisticDistanceTrigger(string name, float minDistance, float maxDistance, float timeScale) {
		this.name = name;
		this.minDistance = minDistance;
		this.maxDistance = maxDistance;
		this.timeScale = timeScale;
	}
}
