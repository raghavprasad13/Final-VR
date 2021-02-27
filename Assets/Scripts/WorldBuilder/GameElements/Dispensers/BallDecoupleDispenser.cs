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

	public override void Dispense() {
		float duration = Random.value * maxTime * C.MillisecondToSecond;
		F.ballDecoupleToggle = 0;
		StartCoroutine(Delay(duration));
		F.ballDecoupleToggle = 1;
	}

	IEnumerator Delay(float delayTime) {
		yield return new WaitForSeconds(delayTime);
	}
}
