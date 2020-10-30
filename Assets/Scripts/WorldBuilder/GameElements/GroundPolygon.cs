using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPolygon {
    List<Vector3> vertices;
    string material;

    public List<Vector3> Vertices {
		get { return vertices; }
	}

    public string Material {
		get { return material; }
	}

	public GroundPolygon(List<Vector3> vertices, string material) {
		this.vertices = vertices;
		this.material = material;
	}
}
