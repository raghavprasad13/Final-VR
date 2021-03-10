using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
	//bool hasEntered = false;
	public OccupationZone occupationZone;
	public Well well;

	private void Start() {
		occupationZone = null;
		well = null;

		if (gameObject.name.Contains("Well"))
			well = TrackFileParser.track.Wells.Find(x => x.WellName.Equals(gameObject.name));
		else
			occupationZone = TrackFileParser.track.OccupationZones.Find(x => x.Name.Equals(gameObject.name));
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Avatar") && occupationZone != null) {
			foreach (Trigger trigger in occupationZone.EntryTriggers)
				trigger.ExecuteTrigger(gameObject.name);
		}

		else if (other.gameObject.CompareTag("Avatar") && well != null)
			well.Dispense();

		if (other.gameObject.CompareTag("Avatar"))
			Data.LogData(other.transform, 1, gameObject.name, "reward zone entry");
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Avatar") && occupationZone != null) {
			foreach (Trigger trigger in occupationZone.ExitTriggers)
				trigger.ExecuteTrigger(gameObject.name);
		}

		if (other.gameObject.CompareTag("Avatar"))
			Data.LogData(other.transform, 1, gameObject.name, "reward zone exit");
	}

	//private void Update() {
	//	if(hasEntered && occupationZone != null) {
	//		//dispenser.Dispense();
	//		print("Inside!");
	//	}
	//}
}
