using System.Collections;
using UnityEngine;

/// <summary>
/// Well class: This is a legacy implementation of Dispenser
/// </summary>
public class Well : MonoBehaviour {
    #region Fields

    Vector2 position;
	readonly string wellName;
    private readonly float capacity;
    private readonly int level;
    private readonly float initialDelay;
    private readonly float pulseDelay;
    private readonly float pulseWidth;
    private readonly float radialBoundaryRadius;
    private readonly float radialTriggerZoneMeshRadius;
    private readonly string radialTriggerZoneMeshMaterial;
    private readonly Pillar pillar;

    #endregion

    #region Properties

    public Vector2 Position {
		get { return position; }
        set { position = value; }
	}

    public string WellName {
        get { return wellName; }
    }

    public float Capacity {
        get { return capacity; }
    }

    public int Level {
        get { return level; }
    }

    public float InitialDelay {
        get { return initialDelay; }
    }

    public float PulseDelay {
        get { return pulseDelay; }
    }

    public float PulseWidth {
        get { return pulseWidth; }
    }

    public float RadialBoundaryRadius {
        get { return radialBoundaryRadius; }
    }

    public float RadialTriggerZoneMeshRadius {
        get { return radialTriggerZoneMeshRadius; }
    }

    public string RadialTriggerZoneMeshMaterial {
        get { return radialTriggerZoneMeshMaterial; }
    }

    public Pillar Pillar {
		get { return pillar; }
	}

    public virtual float Q1Min { get; }

    public virtual float Q1Max { get; }

    public virtual float Q2Min { get; }

    public virtual float Q2Max { get; }

    #endregion

    #region Constructor(s)

    public Well(Vector2 position, string wellName, float capacity, int level, float initialDelay,
                        float pulseDelay, float pulseWidth, float radialBoundaryRadius,
                        float radialTriggerZoneMeshRadius, string radialTriggerZoneMeshMaterial, Pillar pillar) {

		this.position = position;
		this.wellName = wellName;
        this.capacity = capacity;
        this.level = level;
        this.initialDelay = initialDelay;
        this.pulseDelay = pulseDelay;
        this.pulseWidth = pulseWidth;
        this.radialBoundaryRadius = radialBoundaryRadius;
        this.radialTriggerZoneMeshRadius = radialTriggerZoneMeshRadius;
        this.radialTriggerZoneMeshMaterial = radialTriggerZoneMeshMaterial;
        this.pillar = pillar;
    }

    #endregion

    public virtual void Dispense() {
        StartCoroutine(Delay(initialDelay));
        for (int i = 1; i <= capacity; i++) {
            // activate arduino for pulseWidth
            if (i < capacity)
                StartCoroutine(Delay(pulseDelay));
        }
    }

    IEnumerator Delay(float delayTime) {
        yield return new WaitForSeconds(delayTime);
    }
}
