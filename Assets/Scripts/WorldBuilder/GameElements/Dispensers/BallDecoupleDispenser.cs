using System.Collections;
using UnityEngine;
using C = Const.Constants;
using F = Utils.FictracController;

public class BallDecoupleDispenser : Dispenser{
	private readonly float maxTime;

	public float MaxTime {
		get { return maxTime; }
	}

	public BallDecoupleDispenser(string dispenserName, float maxTime) : base(dispenserName) {
		this.maxTime = maxTime;
	}

	public override void Dispense(string callingGameObjectName = null) {
		float duration = Random.value * maxTime * C.MillisecondToSecond;
		F.ballDecoupleToggle = 0;
		if (duration > 0) {
			StartCoroutine(Delay(duration));
			F.ballDecoupleToggle = 1;
		}
	}
}
