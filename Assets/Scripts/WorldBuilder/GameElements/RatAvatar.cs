using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAvatar {
    #region Fields

    Vector2 center;
    Vector2 direction;
    float height;

    #endregion

    #region Properties

    public Vector2 Center {
        get { return center; }
    }

    public Vector2 Direction {
        get { return direction; }
    }

    public float Height {
        get { return height; }
    }

    #endregion

    #region Constructor(s)

    public RatAvatar(float Q1, float Q2, float height, Vector2 direction) {
        this.center = new Vector2(Q1, Q2);
        this.height = height;
        this.direction = direction;
    }

    #endregion
}
