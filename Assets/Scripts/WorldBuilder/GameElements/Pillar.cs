using UnityEngine;

/// <summary>
/// Pillar class
/// </summary>
public class Pillar {
    #region Fields

    Vector2 position;
    float height;
    string material;

    #endregion

    #region Properties

    public Vector2 Position {
		get { return position; }
		set { position = value; }
	}

    public float Height {
        get { return height; }
    }

    public string Material {
        get { return material; }
    }

    #endregion

    #region Constructor(s)

    public Pillar(Vector2 position, float height, string material = null) {
        this.position = position;
        this.height = height;
        this.material = material;
    }

    #endregion
}
