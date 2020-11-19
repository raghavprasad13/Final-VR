using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDispenser : Dispenser {

	#region Fields

	float initialDelay, delay, duration;
	int burstCount;
	float probability;

	#endregion

	#region Properties

	public float InitialDelay {
		get { return initialDelay; }
	}

	public float Delay {
		get { return delay; }
	}

	public float Duration {
		get { return duration; }
	}

	public int BurstCount {
		get { return burstCount; }
	}

	public float Probability {
		get { return probability; }
	}

	#endregion

	#region Constructor

	public RewardDispenser(string dispenserName, float initialDelay, float delay, float duration, int burstCount, float probability) : base(dispenserName) {
		this.initialDelay = initialDelay;
		this.delay = delay;
		this.duration = duration;
		this.burstCount = burstCount;
		this.probability = probability;
	}

	#endregion

	public override void Dispense() {
		/* TODO: Arduino code using Ardity
		 * Followed by trigger
		 */

		print("REWARD DISPENSER");

		//ExecuteTriggers();
	}
}
