﻿using UnityEngine;

public class RandomWell : Well {
	readonly float q1Min;
    readonly float q1Max;
    readonly float q2Min;
    readonly float q2Max;

    public override float Q1Min {
		get { return q1Min; }
	}

    public override float Q1Max {
		get { return q1Max; }
	}

    public override float Q2Min {
		get { return q2Min; }
	}

    public override float Q2Max {
		get { return q2Max; }
	}

    public RandomWell(Vector2 position, string name, float capacity, int level, float initialDelay,
                      float pulseDelay, float pulseWidth, float q1Min, float q1Max, float q2Min,
                      float q2Max, float radialBoundaryRadius, float radialTriggerZoneMeshRadius,
                      string radialTriggerZoneMeshMaterial, Pillar pillar) : base(position, name, capacity, level, initialDelay, pulseDelay,
                                                                                  pulseWidth, radialBoundaryRadius, radialTriggerZoneMeshRadius,
                                                                                  radialTriggerZoneMeshMaterial, pillar) {

        this.q1Max = q1Max;
        this.q2Max = q2Max;
        this.q1Min = q1Min;
        this.q2Min = q2Min;
	}
}