using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for all kinds of triggers
/// Triggers activate actions associated with other objects such as counters and dispensers
/// </summary>
///
/* TODO: Trigger subclasses
 * UniformProbabilityTrigger
 * TimerTrigger
 * ProbabilisticDistanceTrigger
 * 
 */
public class Trigger {
	private readonly string target;
	private readonly bool enable;

	public string Target {
		get { return target; }
	}

	public bool Enable {
		get { return enable; }
	}

	public Trigger(string target, bool enable = true) {
		this.target = target;
		this.enable = enable;
	}

	public void ExecuteTrigger(string callingGameObjectName = null) {
		foreach(Dispenser dispenser in TrackFileParser.track.Dispensers) {
			if (dispenser.DispenserName.Equals(target))
				dispenser.Dispense(callingGameObjectName);
		}
	}
}
