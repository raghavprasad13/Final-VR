using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using SFB;
using Builder;
using System;

/// <summary>
/// This builds the track by building the individual components of the track
/// It initializes a file explorer window for the experimenter to choose the track file
/// Then, by looking at the file name it determines the type of track
/// Depending on the type of track, it calls functions to build the requisite components
/// </summary>
public class TrackBuilder : MonoBehaviour {

    private string trackFilePath;
    private Track track;

    void Start() {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose track file", "Tracks", "track", false);
        trackFilePath = paths[0];

        char pathDelimiter = SystemInfo.operatingSystem.Contains("Windows") ? '\\' : '/';   // Sets the pathDelimiter to '\' in case of Windows, else '/'
        string[] trackFilePathComponents = trackFilePath.Split(pathDelimiter);

        string trackFileName = trackFilePathComponents[trackFilePathComponents.Length - 1];

        if (trackFileName.Contains("random"))
            track = new RandomTrack(trackFilePath);
        /* TODO: Other kinds of tracks */

        string trackFileNameNoExt = trackFileName.Split('.')[0];

        Data.LogFile = trackFileNameNoExt + '-' +
                       DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".csv";

        // The Play Area gameobject will serve as the parent gameobject for all the constituents of the track, in order to have a neat organization
        GameObject playArea = new GameObject("Play Area");
        playArea.transform.position = Vector3.zero;

        BuildTrack(track, playArea);

        SetLayerRecursively(playArea, 10); // Layer 10 is the Play Area layer
    }

    /// <summary>
	/// Iterates through all the attributes of the Track object and passes the data in those attributes to appropriate functions to instantiate gameobjects for them
	/// </summary>
	/// <param name="track">Track object, which contains all the data required to build the gameobjects of the constituent components</param>
	/// <param name="parentObject">parent GameObject which will have all the gameobjects built by this method as its children</param>
    void BuildTrack(Track track, GameObject parentObject) {
        PropertyInfo[] props = track.GetType().GetProperties();

        foreach (PropertyInfo prop in props) {
            print(prop.Name + "\t" + prop.GetValue(track));
            if (prop.Name.Equals("Planes"))
                ComponentsBuilder.Walls(prop.GetValue(track) as List<Plane>, parentObject);
            else if (prop.Name.Equals("GroundPolygonVertices"))
                ComponentsBuilder.Ground(prop.GetValue(track) as List<Vector3>, parentObject);
            else if (prop.Name.Equals("BoundaryVertices"))
                ComponentsBuilder.Boundary(prop.GetValue(track) as List<Vector3>, parentObject);
            else if (prop.Name.Equals("Avatars"))
                ComponentsBuilder.Avatar(prop.GetValue(track) as List<RatAvatar>);
        }
    }

    /// <summary>
	/// Sets the layer for a gameobject and all its descendants
	/// </summary>
	/// <param name="gObject">The gameobject which is at the root of the tree from which the layer assignment is to begin</param>
	/// <param name="layer">The layer to be assigned</param>
    void SetLayerRecursively(GameObject gObject, int layer) {
        gObject.layer = layer;
        foreach (Transform child in gObject.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}