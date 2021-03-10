using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDispenser : Dispenser {

	#region Fields

	private readonly float initialDelay, pulseDelay, duration;
	private readonly int burstCount;
	private readonly float probability;

	#endregion

	#region Properties

	public float InitialDelay {
		get { return initialDelay; }
	}

	public float PulseDelay {
		get { return pulseDelay; }
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

	public RewardDispenser(string dispenserName, float initialDelay, float pulseDelay, float duration, int burstCount, float probability) : base(dispenserName) {
		this.initialDelay = initialDelay;
		this.pulseDelay = pulseDelay;
		this.duration = duration;
		this.burstCount = burstCount;
		this.probability = probability;
	}

	#endregion

	public override void Dispense(string callingGameObjectName = null) {
		/* TODO: Arduino code using Ardity
		 * Followed by trigger
		 */
		StartCoroutine(Delay(initialDelay));
		for (int i = 1; i <= burstCount; i++) {
			// activate arduino for duration
			if (i < burstCount)
				StartCoroutine(Delay(pulseDelay));
		}

		print("REWARD DISPENSER");

		//ExecuteTriggers();
	}
}
