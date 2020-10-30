using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

    #region Fields

    Vector3 center;
    float height;
    string name;
    string material;
    Vector3 facing;
    Vector3 scale;

    #endregion

    #region Properties

    public Vector3 Center {
        get { return center; }
    }

    public float Height {
        get { return height; }
    }

    public string Name {
        get { return name; }
    }

    public string Material {
        get { return material; }
    }

    public Vector3 Facing {
        get { return facing; }
    }

    public Vector3 Scale {
        get { return scale; }
    }

    #endregion

    #region Constructor(s)

    public Plane(Vector3 center, float height, string name, string material, Vector3 facing, Vector3 scale) {
        this.center = center;
        this.height = height;
        this.name = name;
        this.material = material;
        this.facing = facing;
        this.scale = scale;
    }

	#endregion

	public override string ToString() {
        return name;
	}
}
