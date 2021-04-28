using UnityEngine;

public class LightBar {
	private readonly Vector3 center;
	private readonly string type;		// Can be "bar", "torus"
	private readonly string name;
	private readonly float height;
	private readonly float timePeriod;
	private readonly Vector3 scale;
	private readonly Color tintColor;
	private readonly int numberOfCyclesBeforeFlip;

	public Vector3 Center {
		get { return center; }
	}

	public string Type {
		get { return type; }
	}

	public float Height {
		get { return height; }
	}

	public string Name {
		get { return name; }
	}

	public float TimePeriod {
		get { return timePeriod; }
	}

	public Vector3 Scale {
		get { return scale; }
	}

	public Color TintColor {
		get { return tintColor; }
	}

	public LightBar(Vector3 center, string type, string name, float height, float timePeriod, Vector3 scale, float r, float g, float b) {
		this.center = center;
		this.type = type;
		this.name = name;
		this.height = height;
		this.timePeriod = timePeriod;
		this.scale = scale;
		tintColor = new Color(r, g, b);
	}
}
