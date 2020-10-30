using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using C = Const.Constants;

public class TrackFileParser : MonoBehaviour {

	public static Track track;

	public static void parseTrack(XmlElement rootElement) {
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

			track = new SphericalTrack();
		}

		XmlElement element;

		string[] tempArray = { "boundary",
							   "livezone",
							   "groundPolygon" };

		foreach(string str in tempArray) {
			element = rootElement.GetElementsByTagName(str)[0] as XmlElement;
			if (element != null)
				SetVertices(element, str);
		}

		element = rootElement.GetElementsByTagName("ProbabilisticDistanceTrigger")[0] as XmlElement;

		if(element != null) {
			track.ProbDistanceTrigger = new ProbabilisticDistanceTrigger(element.GetAttribute("name"),
																		 float.Parse(element.GetAttribute("minDistance")) * C.CentimeterToMeter,
																		 float.Parse(element.GetAttribute("maxDistance")) * C.CentimeterToMeter,
																		 float.Parse(element.GetAttribute("timeScale")) * C.MillisecondToSecond);
			//parseTriggers();
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
		XmlElement element;

		element = dispensersNode.GetElementsByTagName("audioDispenser")[0] as XmlElement;
		while (element != null) {
			Dispenser dispenser = new AudioDispenser(element.GetAttribute("name"), ParseSound(element));
			dispensers.Add(dispenser);
			element = element.NextSibling as XmlElement;
		}

		/* TODO: Add code for other kinds of dispensers */

		track.Dispensers = dispensers;
	}

	public static Sound ParseSound(XmlElement audioDispenserElement) {
		XmlElement soundElement = audioDispenserElement.GetElementsByTagName("sound")[0] as XmlElement;

		Sound sound = new Sound(soundElement.Attributes["name"].Value,
								soundElement.Attributes["file"].Value,
								float.Parse(soundElement.Attributes["gain"].Value),
								float.Parse(soundElement.Attributes["maxDistance"].Value) * C.CentimeterToMeter,
								float.Parse(soundElement.Attributes["height"].Value) * C.CentimeterToMeter);

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

		element = positionElement.GetElementsByTagName("well")[0] as XmlElement;
		List<Well> wells = new List<Well>();
		while(element != null) {
			wells.Add(ParseWell(element, position));
			element = element.NextSibling as XmlElement;
		}
		track.Wells = wells;

		/* TODO: Occupation zones */

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
	}

	public static Well ParseWell(XmlElement wellElement, Vector2 position) {
		XmlElement element;
		float radialBoundaryRadius = 0f, radialTriggerZoneMeshRadius = 0f;
		string radialTriggerZoneMeshMaterial = null;
		Pillar pillar = null;

		element = wellElement.GetElementsByTagName("radialBoundary")[0] as XmlElement;
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

	private static void SetVertices(XmlElement verticesParentElement, string vertexType) {
		List<Vector3> vertices = new List<Vector3>();

		XmlElement vertexElement = verticesParentElement.FirstChild as XmlElement;
		while(vertexElement != null) {
			vertices.Add(new Vector3(float.Parse(vertexElement.GetAttribute("q1")) * C.CentimeterToMeter,
									 0f,
									 float.Parse(vertexElement.GetAttribute("q2")) * C.CentimeterToMeter));
			vertexElement = vertexElement.NextSibling as XmlElement;
		}

		if (vertexType.Equals("groundPolygon"))
			track.GroundPolygon = new GroundPolygon(vertices, verticesParentElement.GetAttribute("material"));
		else if (vertexType.Equals("boundary"))
			track.Boundary = vertices;
		else if (vertexType.Equals("livezone"))
			track.LiveZone = vertices;
	}
}
