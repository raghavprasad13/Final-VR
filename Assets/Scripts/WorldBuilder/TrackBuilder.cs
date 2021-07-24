using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using SFB;
using Builder;
using System;
using System.Xml;
using System.IO;

using F = Utils.FictracController;
using CsvParser = Utils.CsvFileParser;
using N = Utils.NeuralynxController;

/// TODO:
/// 1. Code to handle XML parsing exceptions, such as when errors occur in writing the track file, that should not be allowed to crash the VR

/// <summary>
/// This builds the track by building the individual components of the track
/// It initializes a file explorer window for the experimenter to choose the track file
/// Then, by looking at the file name it determines the type of track
/// Depending on the type of track, it calls functions to build the requisite components
/// </summary>
public class TrackBuilder : MonoBehaviour {

    private string trackFilePath;
    private float t;    // Used for the pulse wave of room lighting

    void Start() {
        // Start Fictrac
        if (SystemInfo.operatingSystem.Contains("Windows"))
            F.StartFictrac();
        else {
            string command = Path.Combine(Application.streamingAssetsPath, "fictrac", "bin", "fictrac") + " " + Path.Combine(Application.streamingAssetsPath, "fictrac", "config.txt");
            F.StartFictrac(command);
        }

        // Start Arduino that sends TTL pulses to Neuralynx. Commented out until Arduino is ready to be connected, otherwise this will generate an error
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

        if (trackFileName.StartsWith("replay")) {
            F.isReplay = true;

            XmlElement csvFileElement = rootElement.GetElementsByTagName("csvfile")[0] as XmlElement;
            string csvFileName = csvFileElement.GetAttribute("name");
            CsvParser.csvFileName = csvFileName;
            string trackToBeReplayed = csvFileName.Split('-')[0] + ".track";
            trackFilePath = Path.Combine("Assets", "Resources", "Tracks", trackToBeReplayed);

            xmlReader = XmlReader.Create(trackFilePath, readerSettings);
            trackFile.Load(xmlReader);

            rootElement = trackFile.DocumentElement;
        }


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
                if(prop.Name.Equals("Dispensers"))
                    foreach (var item in prop.GetValue(track) as List<Dispenser>)
                        print(item.DispenserName);
                else
                    print(prop.Name + "\t" + prop.GetValue(track));

            if (prop.Name.Equals("Planes"))
                //GameObjectBuilder.Walls(prop.GetValue(track) as List<Plane>, parentObject);
                GameObjectBuilder.Planes(prop.GetValue(track) as List<Plane>, parentObject);

            else if (prop.Name.Equals("GroundPolygon") && prop.GetValue(track) != null)
                GameObjectBuilder.Ground(prop.GetValue(track) as GroundPolygon, parentObject);

            else if (prop.Name.Equals("Bgcolor")) {
                Color bgColor = (Color)prop.GetValue(track);

                if (RenderSettings.skybox.HasProperty("_Tint"))
                    RenderSettings.skybox.SetColor("_Tint", bgColor);
                else if (RenderSettings.skybox.HasProperty("_SkyTint"))
                    RenderSettings.skybox.SetColor("_SkyTint", bgColor);
            }

            else if (prop.Name.Equals("Boundary") && prop.GetValue(track) != null)
                GameObjectBuilder.Boundary(prop.GetValue(track) as List<Vector3>, parentObject);

            else if (prop.Name.Equals("Avatar"))
                GameObjectBuilder.Avatar(prop.GetValue(track) as RatAvatar);

            else if (prop.Name.Equals("Wells"))
                GameObjectBuilder.Wells(prop.GetValue(track) as List<Well>, parentObject);

            else if (prop.Name.Equals("OccupationZones"))
                GameObjectBuilder.OccupationZones(prop.GetValue(track) as List<OccupationZone>, parentObject);

            else if (prop.Name.Equals("OnLoadTriggers")) {
                List<Trigger> onLoadTriggers = prop.GetValue(track) as List<Trigger>;
                if (onLoadTriggers == null)
                    return;

                foreach (Trigger onLoadTrigger in onLoadTriggers)
                    onLoadTrigger.ExecuteTrigger();
                print("OnLoadTriggers loaded");
                print("Ball decouple toggle" + F.ballDecoupleToggle);
            }

            else if (prop.Name.Equals("LightBar"))
                GameObjectBuilder.LightBar(prop.GetValue(track) as LightBar, parentObject);
		}
    }

    /// <summary>
	/// Sets the layer for a gameobject and all its descendants
	/// </summary>
	/// <param name="gObject">The gameobject which is at the root of the tree from which the layer assignment is to begin</param>
	/// <param name="layer">The layer to be assigned</param>
    void SetLayerRecursively(GameObject gObject, int layer) {
        if(gObject.layer == 0)
            gObject.layer = layer;
        foreach (Transform child in gObject.transform)
            SetLayerRecursively(child.gameObject, layer);
    }

	private void Update() {
        // Room pulse code
        if(TrackFileParser.track.PulseTimePeriod > 0)
            PulseRoom();

        if (Input.GetKeyDown(KeyCode.Q)) {
            //N.StopNeuralynxArduino(); // Commented out until Arduino is connected, otherwise this will give an error
            F.StopFictrac();
        }
	}

    /// <summary>
	/// This will make the room pulse with color in a |sin(x)| fashion
	/// </summary>
    private void PulseRoom() {
        Track track = TrackFileParser.track;

        Color amplitude = track.PulseColor;
        float frequency = 1 / track.PulseTimePeriod;
        t += Time.deltaTime;

        Color newColor = amplitude * Mathf.Abs(Mathf.Sin(2 * Mathf.PI * frequency * t));

        if (RenderSettings.skybox.HasProperty("_Tint"))
            RenderSettings.skybox.SetColor("_Tint", newColor);
        else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            RenderSettings.skybox.SetColor("_SkyTint", newColor);
    }
}