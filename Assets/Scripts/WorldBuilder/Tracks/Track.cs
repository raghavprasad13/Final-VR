using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all the different track types
/// </summary>
public abstract class Track {
	Color bgcolor;
	List<Well> wells;
	ProbabilisticDistanceTrigger probDistanceTrigger; // TODO: What are probabilistic distance triggers
	List<Dispenser> dispensers;
	RatAvatar avatar;
	List<Plane> planes;
	GroundPolygon groundPolygon;
	List<Vector3> boundary;
	List<Vector3> liveZone;
	List<OccupationZone> occupationZones;
	List<Trigger> onLoadTriggers;

	public ProbabilisticDistanceTrigger ProbDistanceTrigger {
		get { return probDistanceTrigger; }
		set { probDistanceTrigger = value; }
	}

	public List<Dispenser> Dispensers {
		get { return dispensers; }
		set { dispensers = value; }
	}

	public List<Well> Wells {
		get { return wells; }
		set { wells = value; }
	}

	public RatAvatar Avatar {
		get { return avatar; }
		set { avatar = value; }
	}

	public List<Plane> Planes {
		get { return planes; }
		set { planes = value; }
	}

	public GroundPolygon GroundPolygon {
		get { return groundPolygon; }
		set { groundPolygon = value; }
	}

	public List<Vector3> Boundary {
		get { return boundary; }
		set { boundary = value; }
	}

	public List<Vector3> LiveZone {
		get { return liveZone; }
		set { liveZone = value; }
	}

	public Color Bgcolor {
		get { return bgcolor; }
		set { bgcolor = value; }
	}

	public List<OccupationZone> OccupationZones {
		get { return occupationZones; }
		set { occupationZones = value; }
	}

	public List<Trigger> OnLoadTriggers {
		get { return onLoadTriggers; }
		set { onLoadTriggers = value; }
	}

	public Track() {
		wells = new List<Well>();
		probDistanceTrigger = null;
		dispensers = new List<Dispenser>();
		avatar = null;
		planes = new List<Plane>();
		groundPolygon = null;
		boundary = new List<Vector3>();
		liveZone = new List<Vector3>();
		bgcolor = Color.black;
		occupationZones = new List<OccupationZone>();
		onLoadTriggers = new List<Trigger>();
	}
}
