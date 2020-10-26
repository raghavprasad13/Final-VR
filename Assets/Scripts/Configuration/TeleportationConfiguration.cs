using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to encapsulate teleportation fields
/// </summary>
public class TeleportationConfiguration {
    #region Fields

    float teleportationPositionX;
    float teleportationPositionZ;

    /**
     * teleportationAngleTheta is the angle through which the rat is rotated
     * about the Y-axis, RELATIVE to its current orientation
     */
    float teleportationAngleTheta;     

    #endregion

    #region Properties

    public float TeleportationPositionX {
        get { return teleportationPositionX; }
    }

    public float TeleportationPositionZ {
        get { return teleportationPositionZ; }
    }

    public float TeleportationAngleTheta {
        get { return teleportationAngleTheta; }
    }

    #endregion

    #region Constructor

    public TeleportationConfiguration(float teleportationPositionX, float teleportationPositionZ, float teleportationAngleTheta) {
        this.teleportationPositionX = teleportationPositionX;
        this.teleportationPositionZ = teleportationPositionZ;
        this.teleportationAngleTheta = teleportationAngleTheta;
    }

    #endregion
}
