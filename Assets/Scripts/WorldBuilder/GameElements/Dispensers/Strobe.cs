using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strobe : Dispenser {

	#region Fields

	float lightTime, cycleTime;
	bool isActive;

	#endregion

	#region Properties

	public float LightTime {
		get { return lightTime; }
	}

	public float CycleTime {
		get { return cycleTime; }
	}

	public bool IsActive {
		get { return isActive; }
	}

	#endregion

	public Strobe(string dispenserName, float lightTime, float cycleTime, bool isActive) : base(dispenserName) {
		this.lightTime = lightTime;
		this.cycleTime = cycleTime;
		this.isActive = isActive;
	}

	public override void Dispense() {
		throw new System.NotImplementedException();
	}
}
