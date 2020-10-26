using UnityEngine;

/// <summary>
/// Well class
/// </summary>
public class Well {
    #region Fields

    Vector2 center;
    string name;
    float capacity;
    int level;
    float initialDelay;
    float pulseDelay;
    float pulseWidth;
    bool random;
    float q1Min;
    float q1Max;
    float q2Min;
    float q2Max;
    float radialBoundaryRadius;
    float radialTriggerZoneMeshRadius;
    string radialTriggerZoneMeshMaterial;
    Pillar pillar;

    #endregion

    #region Properties

    public string Name {
        get { return name; }
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

    public bool Random {
        get { return random; }
    }

    public float Q1Min {
        get { return q1Min; }
    }

    public float Q1Max {
        get { return q1Max; }
    }

    public float Q2Min {
        get { return q2Min; }
    }

    public float Q2max {
        get { return q2Max; }
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

    #endregion

    #region Constructor(s)

    public Well(float Q1, float Q2, string name, float capacity, int level, float initialDelay,
                        float pulseDelay, float pulseWidth, bool random, float q1Min, float q1Max, float q2Min,
                        float q2Max, float radialBoundaryRadius, float radialTriggerZoneMeshRadius, string radialTriggerZoneMeshMaterial, Pillar pillar) {
        this.center = new Vector2(Q1, Q2);
        this.name = name;
        this.capacity = capacity;
        this.level = level;
        this.initialDelay = initialDelay;
        this.pulseDelay = pulseDelay;
        this.pulseWidth = pulseWidth;
        this.random = random;
        this.q1Min = q1Min;
        this.q1Max = q1Max;
        this.q2Min = q2Min;
        this.q2Max = q2Max;
        this.radialBoundaryRadius = radialBoundaryRadius;
        this.radialTriggerZoneMeshRadius = radialTriggerZoneMeshRadius;
        this.radialTriggerZoneMeshMaterial = radialTriggerZoneMeshMaterial;
        this.pillar = pillar;
    }

    #endregion

}
