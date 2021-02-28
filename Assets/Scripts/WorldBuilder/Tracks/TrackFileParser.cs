using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using C = Const.Constants;

public class TrackFileParser : MonoBehaviour {

	public static Track track;

	public static void ParseTrack(XmlElement rootElement) {
		string trackType = rootElement.GetAttribute("type").ToLower();
		track = null;

		if (trackType.Equals("angular"))
			track = new AngularTrack();

		if (trackType.Equals("cylinder")) {
			float radius = float.Parse(rootElement.GetAttribute("radius")) * C.CentimeterToMeter;
			float u = float.Parse(rootElement.GetAttribute("u"));

			track = new CylindricalTrack();
		}

		if (trackType.Equals("linear"))
			track = new LinearTrack();

		if (trackType.Equals("plane"))
			track = new PlanarTrack();

		if (trackType.Equals("sphere")) {
			float radius = float.Parse(rootElement.GetAttribute("radius")) * C.CentimeterToMeter;
			float u = float.Parse(rootElement.GetAttribute("u"));
			string material = rootElement.GetAttribute("material");

			track = new SphericalTrack(radius);
		}

		XmlElement element;

		string[] tempArray = { "boundary",
							   "livezone",
							   "groundPolygon" };

		List<Vector3> vertices = new List<Vector3>();
		foreach(string str in tempArray) {
			element = rootElement.GetElementsByTagName(str)[0] as XmlElement;
			if (element != null)
				vertices = SetVertices(element);

			if (str.Equals("groundPolygon"))
				track.GroundPolygon = new GroundPolygon(vertices, element.GetAttribute("material"));
			else if (str.Equals("boundary"))
				track.Boundary = vertices;
			else if (str.Equals("livezone"))
				track.LiveZone = vertices;
		}

		element = rootElement.GetElementsByTagName("bgcolor")[0] as XmlElement;
		if(element != null) {
			float red = float.Parse(element.GetAttribute("r"));
			float green = float.Parse(element.GetAttribute("g"));
			float blue = float.Parse(element.GetAttribute("b"));

			track.Bgcolor = new Color(red, green, blue);
		}

		element = rootElement.GetElementsByTagName("ProbabilisticDistanceTrigger")[0] as XmlElement;

		if(element != null) {
			track.ProbDistanceTrigger = new ProbabilisticDistanceTrigger(element.GetAttribute("name"),
																		 float.Parse(element.GetAttribute("minDistance")) * C.CentimeterToMeter,
																		 float.Parse(element.GetAttribute("maxDistance")) * C.CentimeterToMeter,
																		 float.Parse(element.GetAttribute("timeScale")) * C.MillisecondToSecond);
			//ParseTriggers();
		}

		/* TODO
		 * UniformProbabilityTriggers
		 * TimerTriggers
		 */

		element = rootElement.GetElementsByTagName("dispensers")[0] as XmlElement;
		if (element != null)
			ParseDispensers(element);

		element = rootElement.GetElementsByTagName("positions")[0] as XmlElement;
		if (element != null)
			ParsePositions(element);
	}

	public static void ParseDispensers(XmlElement dispensersNode) {
		List<Dispenser> dispensers = new List<Dispenser>();
		XmlNodeList elements;
		XmlElement element;

		// AudioDispenser
		elements = dispensersNode.GetElementsByTagName("audiodispenser");
		foreach (XmlElement element1 in elements) {
			Dispenser dispenser = new AudioDispenser(element1.GetAttribute("name"), ParseSound(element1));
			dispensers.Add(dispenser);
			//element = element.NextSibling as XmlElement;
		}

		// Strobe
		element = dispensersNode.GetElementsByTagName("strobe")[0] as XmlElement;
		if (element != null) {
			string name = element.GetAttribute("name");
			float lightTime = float.Parse(element.GetAttribute("lightTime")) * C.MillisecondToSecond;
			float cycleTime = float.Parse(element.GetAttribute("cycleTime")) * C.MillisecondToSecond;
			bool isActive = bool.Parse(element.GetAttribute("active"));

			Dispenser dispenser = new Strobe(name, lightTime, cycleTime, isActive);
			dispensers.Add(dispenser);
		}

		// RewardDispenser
		elements = dispensersNode.GetElementsByTagName("rewarddispenser");
		foreach(XmlElement element1 in elements) {
			string name = element1.GetAttribute("name");
			float initialDelay = float.Parse(element1.GetAttribute("initialDelay")) * C.MillisecondToSecond;
			float delay = float.Parse(element1.GetAttribute("delay")) * C.MillisecondToSecond;
			float duration = float.Parse(element1.GetAttribute("duration")) * C.MillisecondToSecond;
			int burstCount = int.Parse(element1.GetAttribute("burstCount"));
			float probability = float.Parse(element1.GetAttribute("probability"));

			Dispenser dispenser = new RewardDispenser(name, initialDelay, delay, duration, burstCount, probability);
			dispenser.Triggers = ParseTriggers(element1);
			dispensers.Add(dispenser);
			//element = element.NextSibling as XmlElement;
		}

		// BlackoutDispenser
		element = dispensersNode.GetElementsByTagName("blackoutdispenser")[0] as XmlElement;
		if(element != null) {
			string name = element.GetAttribute("name");
			float maxTime = float.Parse(element.GetAttribute("maxTime"));

			dispensers.Add(new BlackoutDispenser(name, maxTime));
		}

		// DiscreteTeleportDispenser
		elements = dispensersNode.GetElementsByTagName("DiscreTeleportDispenser");
		foreach(XmlElement element1 in elements) {
			string name = element1.GetAttribute("name");
			bool sequential = bool.Parse(element1.GetAttribute("sequential"));
			float delay = float.Parse(element1.GetAttribute("delay"));

			List<Tuple<Vector3, Vector2>> destinations = new List<Tuple<Vector3, Vector2>>();

			XmlNodeList destinationElements = element1.GetElementsByTagName("destination");
			foreach(XmlElement destinationElement in destinationElements) {

				XmlElement position = destinationElement.FirstChild as XmlElement;
				Vector3 destinationPosition = new Vector3(float.Parse(position.GetAttribute("q1")), C.AvatarBaseHeight, float.Parse(position.GetAttribute("q2"))) * C.CentimeterToMeter;

				XmlElement orientation = destinationElement.LastChild as XmlElement;
				Vector2 destinationOrientation = new Vector2(float.Parse(orientation.GetAttribute("q1")), float.Parse(orientation.GetAttribute("q2")));

				destinations.Add(new Tuple<Vector3, Vector2>(destinationPosition, destinationOrientation));
			}

			Dispenser dispenser = new DiscreteTeleportDispenser(name, sequential, delay, destinations);
			dispenser.Triggers = ParseTriggers(element);
			dispensers.Add(dispenser);
		}

		// Hider
		elements = dispensersNode.GetElementsByTagName("hider");
		foreach(XmlElement element1 in elements) {
			string name = element1.GetAttribute("name");
			List<string> targets = new List<string>();

			XmlElement targetElement = element1.GetElementsByTagName("target")[0] as XmlElement;
			while(targetElement != null) {
				targets.Add(targetElement.GetAttribute("name"));
				targetElement = targetElement.NextSibling as XmlElement;
			}

			Dispenser dispenser = new Hider(name, targets);
			dispensers.Add(dispenser);
		}
 
		/* TODO: Add code for other kinds of dispensers
		 * Gain dispenser
		 * Audio blackout dispenser
		 * Ball Decouple Dispenser
		 * Random rotation dispenser
		 * Random mover
		 * Discrete mover
		 * 
		 */

		track.Dispensers = dispensers;
	}

	public static Sound ParseSound(XmlElement audioDispenserElement) {
		XmlElement soundElement = audioDispenserElement.GetElementsByTagName("sound")[0] as XmlElement;

		if (soundElement == null)
			print("Its NULL");
		print("InParseSound: " + soundElement.Name);
		//print(soundElement.GetAttribute("name"));

		Sound sound = new Sound(soundElement.GetAttribute("name"),
								soundElement.GetAttribute("file"),
								float.Parse(soundElement.GetAttribute("gain")),
								float.Parse(soundElement.GetAttribute("maxdistance")) * C.CentimeterToMeter,
								float.Parse(soundElement.GetAttribute("height")) * C.CentimeterToMeter);

		print(soundElement.GetAttribute("name"));

		return sound;
	}

	public static void ParsePositions(XmlElement positionsElement) {
		XmlElement positionElement = positionsElement.FirstChild as XmlElement;
		while (positionElement != null ) {
			ParsePosition(positionElement);
			positionElement = positionElement.NextSibling as XmlElement;
		}
	}

	public static void ParsePosition(XmlElement positionElement) {
		Vector2 position = new Vector2(float.Parse(positionElement.GetAttribute("q1")) * C.CentimeterToMeter,
									   float.Parse(positionElement.GetAttribute("q2")) * C.CentimeterToMeter);

		XmlElement element;

		// Wells
		element = positionElement.GetElementsByTagName("well")[0] as XmlElement;
		List<Well> wells = new List<Well>();
		while(element != null) {
			wells.Add(ParseWell(element, position));
			element = element.NextSibling as XmlElement;
		}
		track.Wells = wells;

		// Occupation zones
		element = positionElement.GetElementsByTagName("occupationzone")[0] as XmlElement;
		if(element != null) {
			ParseOccupationZone(element, position);
		}

		element = positionElement.GetElementsByTagName("avatar")[0] as XmlElement;
		if (element != null)
			ParseAvatar(element, position);

		/* TODO
		 * Sound
		 * Billboard
		 * Mesh
		 * disc
		 * ring
		 * smell
		 */

		element = positionElement.GetElementsByTagName("plane")[0] as XmlElement;
		//List<Plane> planes = new List<Plane>();
		while (element != null) {
			track.Planes.Add(ParsePlane(element, position));
			//print("planes.Count from TrackFileParser: " + planes.Count);
			element = element.NextSibling as XmlElement;
		}
		//print("planes.COunt from TrackFileParser: " + planes.Count);
		//track.Planes = planes;
		//print("track.Planes.COunt from TrackFileParser: " + track.Planes.Count);

		// Bar of light
		element = positionElement.GetElementsByTagName("lightbar")[0] as XmlElement;
		if (element != null)
			ParseLightBar(element, position);
	}

	public static Well ParseWell(XmlElement wellElement, Vector2 position) {
		XmlElement element;
		float radialBoundaryRadius = 0f, radialTriggerZoneMeshRadius = 0f;
		string radialTriggerZoneMeshMaterial = null;
		Pillar pillar = null;

		element = wellElement.GetElementsByTagName("radialboundary")[0] as XmlElement;
		if (element != null)
			radialBoundaryRadius = float.Parse(element.GetAttribute("radius")) * C.CentimeterToMeter;

		element = wellElement.GetElementsByTagName("radialTriggerZoneMesh")[0] as XmlElement;
		if(element != null) {
			radialTriggerZoneMeshRadius = float.Parse(element.GetAttribute("radius")) * C.CentimeterToMeter;
			radialTriggerZoneMeshMaterial = element.GetAttribute("material");
		}

		element = wellElement.GetElementsByTagName("pillar")[0] as XmlElement;
		if (element != null)
			pillar = ParsePillar(element, position);

		string name = wellElement.GetAttribute("name");
		float capacity = float.Parse(wellElement.GetAttribute("capacity"));
		int level = int.Parse(wellElement.GetAttribute("level"));
		float initialDelay = float.Parse(wellElement.GetAttribute("initialDelay")) * C.MillisecondToSecond;
		float pulseDelay = float.Parse(wellElement.GetAttribute("pulseDelay")) * C.MillisecondToSecond;
		float pulseWidth = float.Parse(wellElement.GetAttribute("pulseWidth")) * C.MillisecondToSecond;


		if (bool.Parse(wellElement.GetAttribute("random")))
			return new RandomWell(position, name, capacity, level, initialDelay, pulseDelay, pulseWidth,
								  float.Parse(wellElement.GetAttribute("q1min")) * C.CentimeterToMeter,
								  float.Parse(wellElement.GetAttribute("q1max")) * C.CentimeterToMeter,
								  float.Parse(wellElement.GetAttribute("q2min")) * C.CentimeterToMeter,
								  float.Parse(wellElement.GetAttribute("q2max")) * C.CentimeterToMeter,
								  radialBoundaryRadius, radialTriggerZoneMeshRadius,
								  radialTriggerZoneMeshMaterial, pillar);

		return new Well(position, name, capacity, level, initialDelay, pulseDelay, pulseWidth,
						radialBoundaryRadius, radialTriggerZoneMeshRadius, radialTriggerZoneMeshMaterial, pillar);
	}

	public static Pillar ParsePillar(XmlElement pillarElement, Vector2 position) {
		float height = float.Parse(pillarElement.GetAttribute("height")) * C.CentimeterToMeter;
		string attributes = pillarElement.Attributes.ToString();
		string material = null;
		if (attributes.Contains("material"))
			material = pillarElement.Attributes["material"].Value;

		return new Pillar(position, height, material);
	}

	public static void ParseAvatar(XmlElement avatarElement, Vector2 position) {
		float height = float.Parse(avatarElement.GetAttribute("height")) * C.CentimeterToMeter;
		Vector2 direction = Vector2.zero;

		XmlElement element;

		element = avatarElement.GetElementsByTagName("direction")[0] as XmlElement;
		if (element != null)
			direction = new Vector2(float.Parse(element.GetAttribute("q1")), float.Parse(element.GetAttribute("q2")));

		track.Avatar = new RatAvatar(position, height, direction);
	}

	public static Plane ParsePlane(XmlElement planeElement, Vector2 position) {
		string name = planeElement.GetAttribute("name");
		float height = float.Parse(planeElement.GetAttribute("height")) * C.CentimeterToMeter;
		string material = planeElement.GetAttribute("material");
		Vector3 facing = Vector3.zero;
		Vector3 scale = Vector3.zero;

		XmlElement element;
		element = planeElement.GetElementsByTagName("facing")[0] as XmlElement;
		if (element != null)
			facing = new Vector3(float.Parse(element.GetAttribute("x")), float.Parse(element.GetAttribute("y")), float.Parse(element.GetAttribute("z")));


		element = planeElement.GetElementsByTagName("scale")[0] as XmlElement;
		if(element != null)
			scale = new Vector3(float.Parse(element.GetAttribute("x")) * C.CentimeterToMeter,
								float.Parse(element.GetAttribute("y")) * C.CentimeterToMeter,
								float.Parse(element.GetAttribute("z")) * C.CentimeterToMeter);

		Plane plane = new Plane(new Vector3(position.x, 0, position.y), height, name, material, facing, scale);
		print("From TrackFileParser: " + plane);
		return plane;
	}

	private static List<Vector3> SetVertices(XmlElement verticesParentElement) {
		List<Vector3> vertices = new List<Vector3>();

		XmlElement vertexElement = verticesParentElement.FirstChild as XmlElement;
		while(vertexElement != null) {
			vertices.Add(new Vector3(float.Parse(vertexElement.GetAttribute("q1")) * C.CentimeterToMeter,
									 0f,
									 float.Parse(vertexElement.GetAttribute("q2")) * C.CentimeterToMeter));
			vertexElement = vertexElement.NextSibling as XmlElement;
		}

		return vertices;
	}

	public static List<Trigger> ParseTriggers(XmlElement triggersParentElement) {
		List<Trigger> triggers = new List<Trigger>();

		XmlElement element = triggersParentElement.GetElementsByTagName("trigger")[0] as XmlElement;
		while(element != null) {
			triggers.Add(ParseTrigger(element));
			element = element.NextSibling as XmlElement;
		}

		return triggers;
	}

	public static Trigger ParseTrigger(XmlElement triggerElement) {
		string target = triggerElement.GetAttribute("target");
		if (triggerElement.HasAttribute("enabled")) {
			bool enable = bool.Parse(triggerElement.GetAttribute("enabled"));
			return new Trigger(target, enable);
		}

		return new Trigger(target);
	}

	public static void ParseOccupationZone(XmlElement occupationZoneElement, Vector2 position) {
		string name = occupationZoneElement.GetAttribute("name");
		bool isActive = bool.Parse(occupationZoneElement.GetAttribute("active"));

		XmlElement entryTriggersParentElement = occupationZoneElement.GetElementsByTagName("onentry")[0] as XmlElement;
		List<Trigger> entryTriggers = ParseTriggers(entryTriggersParentElement);

		XmlElement exitTriggersParentElement = occupationZoneElement.GetElementsByTagName("onexit")[0] as XmlElement;
		List<Trigger> exitTriggers = ParseTriggers(exitTriggersParentElement);

		float minTime = float.NegativeInfinity;
		if (occupationZoneElement.HasAttribute("mintime"))
			minTime = float.Parse(occupationZoneElement.GetAttribute("mintime")) * C.MillisecondToSecond;

		XmlElement radialBoundaryElement = occupationZoneElement.GetElementsByTagName("radialboundary")[0] as XmlElement;
		bool isRadialBoundary = radialBoundaryElement != null;

		if (isRadialBoundary) {
			float radialBoundaryRadius = float.Parse(radialBoundaryElement.GetAttribute("radius")) * C.CentimeterToMeter;
			track.OccupationZones.Add(new OccupationZone(name, new Vector3(position.x, 0f, position.y),
														 isActive, isRadialBoundary, entryTriggers,
														 exitTriggers, minTime, radialBoundaryRadius));
		}

		else {
			XmlElement polygonBoundaryVerticesParentElement = occupationZoneElement.GetElementsByTagName("polygonboundary")[0] as XmlElement;
			//print(occupationZoneElement.GetAttribute("name"));
			List<Vector3> polygonBoundaryVertices = SetVertices(polygonBoundaryVerticesParentElement);
			track.OccupationZones.Add(new OccupationZone(name, new Vector3(position.x, 0f, position.y),
														 isActive, isRadialBoundary, entryTriggers, exitTriggers,
														 minTime, polygonBoundaryVertices: polygonBoundaryVertices));
		}
	}

	public static void ParseLightBar(XmlElement lightBarElement, Vector2 position) {
		string name = lightBarElement.GetAttribute("name");
		float timePeriod = float.Parse(lightBarElement.GetAttribute("timeperiod")) * C.MillisecondToSecond;
		float height = float.Parse(lightBarElement.GetAttribute("height")) * C.CentimeterToMeter;

		XmlElement tintColorElement = lightBarElement.GetElementsByTagName("tintcolor")[0] as XmlElement;
		float r = float.Parse(tintColorElement.GetAttribute("r"));
		float g = float.Parse(tintColorElement.GetAttribute("g"));
		float b = float.Parse(tintColorElement.GetAttribute("b"));

		track.LightBar = new LightBar(new Vector3(position.x, 0, position.y), name, timePeriod, height, r, g, b);
	}
}
