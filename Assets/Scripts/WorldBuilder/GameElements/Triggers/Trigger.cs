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
 */
public class Trigger {
	string target;
	bool enable;

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

	public void ExecuteTrigger() {
		foreach(Dispenser dispenser in TrackFileParser.track.Dispensers) {
			if (dispenser.DispenserName.Equals(target))
				dispenser.Dispense();
		}
	}
}
