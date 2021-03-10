using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Abstract Dispenser base class which must be derived by different types of Dispenser subclasses
/// </summary>

/* TODO: Dispenser subclasses
 * DiscreteTeleportDispenser - DONE
 * BlackoutDispenser - DONE
 * RotationDispenser - DONE
 * BallDecoupleDispenser - DONE
 * RewardDispenser - DONE
 * Hider - DONE
 * Strobe - DONE
 * AudioDispenser - DONE
 * AudioBlackoutDispenser
 * DiscreteMoverDispenser
 */
public abstract class Dispenser : MonoBehaviour {	// Inheriting from MonoBehaviour here is not a good practice because Dispenser will be instantiated. Instead, have a separate dedicated MonoBehaviour inheriting class just for coroutines
	string dispenserName;
    List<Trigger> triggers;

    public string DispenserName {
        get { return dispenserName; }
        set { dispenserName = value; }
    }

    public List<Trigger> Triggers {
		get { return triggers; }
		set { triggers = value; }
	}

	public Dispenser(string dispenserName, List<Trigger> triggers = null) {
		this.dispenserName = dispenserName;
		this.triggers = triggers;
	}

	public void ExecuteTriggers() {
		if (triggers == null)
			return;

		foreach (Trigger trigger in triggers)
			trigger.ExecuteTrigger();
	}

	public abstract void Dispense(string callingGameObjectName = null);

	public IEnumerator Delay(float delayTime) {
		yield return new WaitForSeconds(delayTime);
	}

	//public abstract void LogToCsv();
}
