using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils {
    static ConfigurationData configurationData;
    static TrackFileData trackFileData;

    #region Properties

    public static List<TeleportationConfiguration> TeleportationConfigurations {
        get { return configurationData.TeleportationConfigurations; }
    }

    public static List<Vector3> BoundaryVertices {
        get { return trackFileData.BoundaryVertices; }
    }

    public static List<Vector3> LiveZoneVertices {
        get { return trackFileData.LiveZoneVertices; }
    }

    public static List<Vector3> GroundPolygonVertices {
        get { return trackFileData.GroundPolygonVertices; }
    }

    public static string GroundPolygonMaterialName {
        get { return trackFileData.GroundPolygonMaterialName; }
    }

    public static List<Dispenser> Dispensers {
        get { return trackFileData.Dispensers; }
    }

    public static List<RatAvatar> Avatars {
        get { return trackFileData.Avatars; }
    }

    public static List<Plane> Planes {
        get { return trackFileData.Planes; }
    }

    public static List<Well> Wells {
        get { return trackFileData.Wells; }
    }

    #endregion

    public static void Initialize() {
        trackFileData = new TrackFileData();
        configurationData = new ConfigurationData();
    }
}
