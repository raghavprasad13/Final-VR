using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using SFB;
using Builder;
using System;
using System.Xml;

using F = Utils.FictracController;
using N = Utils.NeuralynxController;

/// <summary>
/// This builds the track by building the individual components of the track
/// It initializes a file explorer window for the experimenter to choose the track file
/// Then, by looking at the file name it determines the type of track
/// Depending on the type of track, it calls functions to build the requisite components
/// </summary>
public class TrackBuilder : MonoBehaviour {

    private string trackFilePath;

    void Start() {
        // Start Fictrac
        if (SystemInfo.operatingSystem.Contains("Windows"))
            F.StartFictrac();
        else {
            string command = "/Users/raghavprasad/Work/BITS/4-1/Thesis/fictrac/bin/fictrac /Users/raghavprasad/Work/BITS/4-1/Thesis/fictrac/closed_loop_forward_backward/config.txt";
            F.StartFictrac(command);
        }

        // Start Arduino that sends TTL pulses to Neuralynx
        //N.StartNeuralynxArduino();

        string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose track file", "Tracks", "track", false);
        trackFilePath = paths[0];

        char pathDelimiter = SystemInfo.operatingSystem.Contains("Windows") ? '\\' : '/';   // Sets the pathDelimiter to '\' in case of Windows, else '/'
        string[] trackFilePathComponents = trackFilePath.Split(pathDelimiter);

        string trackFileName = trackFilePathComponents[trackFilePathComponents.Length - 1];

        XmlReaderSettings readerSettings = new XmlReaderSettings {
            IgnoreComments = true
        };
        XmlReader xmlReader = XmlReader.Create(trackFilePath, readerSettings);
        XmlDocument trackFile = new XmlDocument();
        trackFile.Load(xmlReader);

        XmlElement rootElement = trackFile.DocumentElement;

        TrackFileParser.ParseTrack(rootElement);

        string trackFileNameNoExt = trackFileName.Split('.')[0];

        Data.LogFile = trackFileNameNoExt + '-' +
                       DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".csv";

        // The Play Area gameobject will serve as the parent gameobject for all the constituents of the track, in order to have a neat organization
        GameObject playArea = new GameObject("Play Area");
        playArea.transform.position = Vector3.zero;

        print("TrackFileParser.track.Planes: " + TrackFileParser.track.Planes.Count);

        BuildTrack(TrackFileParser.track, playArea);

        SetLayerRecursively(playArea, 10); // Layer 10 is the Play Area layer
    }

    /// <summary>
	/// Iterates through all the attributes of the Track object and passes the data in those attributes to appropriate functions to instantiate gameobjects for them
	/// </summary>
	/// <param name="track">Track object, which contains all the data required to build the gameobjects of the constituent components</param>
	/// <param name="parentObject">parent GameObject which will have all the gameobjects built by this method as its children</param>
    void BuildTrack(Track track, GameObject parentObject) {
        PropertyInfo[] properties = track.GetType().GetProperties();

        foreach (PropertyInfo prop in properties) {
            if (prop.GetValue(track) == null)
                print(prop.Name + "\t" + "null");
            else
                print(prop.Name + "\t" + prop.GetValue(track));

            if (prop.Name.Equals("Planes"))
                //GameObjectBuilder.Walls(prop.GetValue(track) as List<Plane>, parentObject);
                GameObjectBuilder.Planes(prop.GetValue(track) as List<Plane>, parentObject);

            else if (prop.Name.Equals("GroundPolygon"))
                GameObjectBuilder.Ground(prop.GetValue(track) as GroundPolygon, parentObject);

            else if (prop.Name.Equals("Bgcolor")) {
                /* TODO: Change skybox color to Bgcolor */
            }

            else if (prop.Name.Equals("Boundary"))
                GameObjectBuilder.Boundary(prop.GetValue(track) as List<Vector3>, parentObject);

            else if (prop.Name.Equals("Avatar"))
                GameObjectBuilder.Avatar(prop.GetValue(track) as RatAvatar);

            else if (prop.Name.Equals("Wells"))
                GameObjectBuilder.Wells(prop.GetValue(track) as List<Well>, parentObject);

            else if (prop.Name.Equals("OccupationZones"))
                GameObjectBuilder.OccupationZones(prop.GetValue(track) as List<OccupationZone>, parentObject);
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

	private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            F.StopFictrac();
        }
	}
}