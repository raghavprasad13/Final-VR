using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupationZone {
	string name;
	Vector3 position;
	bool isActive;
	float minTime;
	float radialBoundaryRadius;
	List<Vector3> polygonBoundaryVertices;
	bool isRadialBoundary;
	List<Trigger> entryTriggers, exitTriggers;

	public string Name {
		get { return name; }
	}

	public Vector3 Position {
		get { return position; }
	}

	public bool IsActive {
		get { return isActive; }
	}

	public float MinTime {
		get { return minTime; }
	}

	public bool IsRadialBoundary {
		get { return isRadialBoundary; }
	}

	public float RadialBoundaryRadius {
		get { return radialBoundaryRadius; }
	}

	public List<Vector3> PolygonBoundaryVertices {
		get { return polygonBoundaryVertices; }
	}

	public List<Trigger> EntryTriggers {
		get { return entryTriggers; }
	}

	public List<Trigger> ExitTriggers {
		get { return exitTriggers; }
	}

	public OccupationZone(string name, Vector3 position, bool isActive, bool isRadialBoundary, List<Trigger> entryTriggers, List<Trigger> exitTriggers,
						  float minTime = float.NegativeInfinity, float radialBoundaryRadius = 0f, List<Vector3> polygonBoundaryVertices = null) {
		this.name = name;
		this.position = position;
		this.isActive = isActive;
		this.minTime = minTime;
		this.isRadialBoundary = isRadialBoundary;
		this.entryTriggers = entryTriggers;
		this.exitTriggers = exitTriggers;
		this.radialBoundaryRadius = radialBoundaryRadius;
		this.polygonBoundaryVertices = polygonBoundaryVertices;
	}
}
