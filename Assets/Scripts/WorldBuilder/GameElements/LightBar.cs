using UnityEngine;

public class LightBar {
	private readonly Vector3 center;
	private readonly string name;
	private readonly float timePeriod;
	private readonly float height;
	private readonly Color tintColor;

	public Vector3 Center {
		get { return center; }
	}

	public string Name {
		get { return name; }
	}

	public float TimePeriod {
		get { return timePeriod; }
	}

	public float Height {
		get { return height; }
	}

	public Color TintColor {
		get { return tintColor; }
	}

	public LightBar(Vector3 center, string name, float timePeriod, float height, float r, float g, float b) {
		this.center = center;
		this.name = name;
		this.timePeriod = timePeriod;
		this.height = height;
		tintColor = new Color(r, g, b);
	}
}
