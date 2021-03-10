using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDispenser : Dispenser {
    private readonly float minRotation, maxRotation;
    private readonly Vector3 referenceDirection;

    public float MinRotation {
		get { return minRotation; }
	}

    public float MaxRotation {
		get { return maxRotation; }
	}

    public Vector3 ReferenceDirection {
        get { return referenceDirection; }
	}

    public RotationDispenser(string dispenserName, float minRotation, float maxRotation, Vector3 referenceDirection) : base(dispenserName) {
        this.minRotation = minRotation;
        this.maxRotation = maxRotation;
        this.referenceDirection = referenceDirection;
	}

    public override void Dispense(string callingGameObjectName = null) {
        GameObject avatar = GameObject.Find("Avatar");

        float rotationAngleMagnitude = Random.Range(minRotation, maxRotation);
        Vector3 newFacingDirection = Quaternion.AngleAxis(rotationAngleMagnitude, Vector3.up) * referenceDirection;

        avatar.transform.rotation = Quaternion.LookRotation(newFacingDirection);

        // Uncomment the line below if there are any instances of RotationDispenser with triggers
        // ExecuteTriggers();
    }
}
