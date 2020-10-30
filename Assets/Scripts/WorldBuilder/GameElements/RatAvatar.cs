using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAvatar {
    #region Fields

    Vector2 position;
    Vector2 direction;
	readonly float height;

    #endregion

    #region Properties

    public Vector2 Position {
        get { return position; }
    }

    public Vector2 Direction {
        get { return direction; }
    }

    public float Height {
        get { return height; }
    }

    #endregion

    #region Constructor(s)

    public RatAvatar(Vector2 position, float height, Vector2 direction) {
        this.position = position;
        this.height = height;
        this.direction = direction;
    }

    #endregion
}
