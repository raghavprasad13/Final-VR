using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Abstract Dispenser base class which must be derived by different types of Dispenser subclasses
/// </summary>
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

	public Dispenser(string dispenserName) {
		this.dispenserName = dispenserName;
		triggers = null;
	}

	public void ExecuteTriggers() {
		if (Triggers == null)
			return;

		foreach (Trigger trigger in Triggers)
			trigger.ExecuteTrigger();
	}

	public abstract void Dispense();

	//public abstract void LogToCsv();
}
