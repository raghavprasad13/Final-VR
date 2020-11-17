using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
