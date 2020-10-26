using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using Const;

/// <summary>
/// This class takes care of "random" tracks
/// It has attributes that correspond to the components that constitute the random track world
/// It parses the track file to create high level representations of the data within the track file
/// </summary>
public class RandomTrack : Track {
    #region Fields

    List<Vector3> boundaryVertices;
    List<Vector3> liveZoneVertices;
    List<Vector3> groundPolygonVertices;
    string groundPolygonMaterialName = null;
    List<Dispenser> dispensers;
    List<RatAvatar> avatars;
    List<Plane> planes;
    List<Well> wells;

    #endregion

    #region Properties

    public List<Vector3> BoundaryVertices {
        get { return boundaryVertices; }
    }

    public List<Vector3> LiveZoneVertices {
        get { return liveZoneVertices; }
    }

    public List<Vector3> GroundPolygonVertices {
        get { return groundPolygonVertices; }
    }

    public string GroundPolygonMaterialName {
        get { return groundPolygonMaterialName; }
    }

    public List<Dispenser> Dispensers {
        get { return dispensers; }
    }

    public List<RatAvatar> Avatars {
        get { return avatars; }
    }

    public List<Plane> Planes {
        get { return planes; }
    }

    public List<Well> Wells {
        get { return wells; }
    }

	#endregion

	#region Constructor

	public RandomTrack(string filePath) {
		Type = "random";
        FilePath = filePath;

        boundaryVertices = new List<Vector3>();
        liveZoneVertices = new List<Vector3>();
        groundPolygonVertices = new List<Vector3>();
        dispensers = new List<Dispenser>();
        avatars = new List<RatAvatar>();
        planes = new List<Plane>();
        wells = new List<Well>();

		XmlReaderSettings readerSettings = new XmlReaderSettings {
			IgnoreComments = true
		};
		XmlReader xmlReader = XmlReader.Create(Path.Combine(Application.streamingAssetsPath, FilePath), readerSettings);

        XmlDocument trackFile = new XmlDocument();
        trackFile.Load(xmlReader);

        foreach (XmlNode xmlNode in trackFile.DocumentElement) {
            if (xmlNode.Name.Equals("boundary"))
                SetVertices(xmlNode, boundaryVertices);
            else if (xmlNode.Name.Equals("livezone"))
                SetVertices(xmlNode, liveZoneVertices);
            else if (xmlNode.Name.Equals("groundPolygon")) {
                groundPolygonMaterialName = xmlNode.Attributes[0].Value;
                SetVertices(xmlNode, groundPolygonVertices);
            }
            else if (xmlNode.Name.Equals("bgcolor")) { /* do nothing for now */ }
            else if (xmlNode.Name.Equals("dispensers"))
                SetDispensers(xmlNode);
            else if (xmlNode.Name.Equals("positions"))
                SetPositions(xmlNode);
        }
    }

    #endregion

    #region Methods

    /// <summary>
	/// Creates objects of the components in the "position" nodes in the track file
	/// </summary>
	/// <param name="positionsNode">XmlNode object representing the position node in the track file</param>
    private void SetPositions(XmlNode positionsNode) {
        XmlNode firstChildNode;
        foreach (XmlNode positionNode in positionsNode) {
            firstChildNode = positionNode.FirstChild;

            if (firstChildNode.Name.Equals("avatar"))
                avatars.Add(new RatAvatar(float.Parse(positionNode.Attributes[0].Value) * Constants.CentimeterToMeter,
                                            float.Parse(positionNode.Attributes[1].Value) * Constants.CentimeterToMeter,
                                            float.Parse(firstChildNode.Attributes[0].Value) * Constants.CentimeterToMeter,
                                            new Vector2(float.Parse(firstChildNode["direction"].Attributes[0].Value),
                                                        float.Parse(firstChildNode["direction"].Attributes[1].Value))));

            else if (firstChildNode.Name.Equals("plane"))
                planes.Add(new Plane(float.Parse(positionNode.Attributes[0].Value) * Constants.CentimeterToMeter,
                                        float.Parse(positionNode.Attributes[1].Value) * Constants.CentimeterToMeter,
                                        float.Parse(firstChildNode.Attributes[1].Value) * Constants.CentimeterToMeter,
                                        firstChildNode.Attributes[0].Value,
                                        firstChildNode.Attributes[2].Value,
                                        new Vector3(float.Parse(firstChildNode["facing"].Attributes[0].Value),
                                                    float.Parse(firstChildNode["facing"].Attributes[1].Value),
                                                    float.Parse(firstChildNode["facing"].Attributes[2].Value)),
                                        new Vector3(float.Parse(firstChildNode["scale"].Attributes[0].Value) * Constants.CentimeterToMeter,
                                                    float.Parse(firstChildNode["scale"].Attributes[1].Value) * Constants.CentimeterToMeter,
                                                    float.Parse(firstChildNode["scale"].Attributes[2].Value) * Constants.CentimeterToMeter)));

            else if (firstChildNode.Name.Equals("well")) {
                foreach (XmlNode well in positionNode)
                    wells.Add(new Well(float.Parse(positionNode.Attributes[0].Value) * Constants.CentimeterToMeter,
                                        float.Parse(positionNode.Attributes[1].Value) * Constants.CentimeterToMeter,
                                        well.Attributes[0].Value,
                                        float.Parse(well.Attributes[1].Value),
                                        int.Parse(well.Attributes[2].Value),
                                        float.Parse(well.Attributes[3].Value) * Constants.MillisecondToSecond,
                                        float.Parse(well.Attributes[4].Value) * Constants.MillisecondToSecond,
                                        float.Parse(well.Attributes[5].Value) * Constants.MillisecondToSecond,
                                        well.Attributes[6].Value.Equals("true"),
                                        float.Parse(well.Attributes[7].Value) * Constants.CentimeterToMeter,
                                        float.Parse(well.Attributes[8].Value) * Constants.CentimeterToMeter,
                                        float.Parse(well.Attributes[9].Value) * Constants.CentimeterToMeter,
                                        float.Parse(well.Attributes[10].Value) * Constants.CentimeterToMeter,
                                        float.Parse(well["radialboundary"].Attributes[0].Value) * Constants.CentimeterToMeter,
                                        float.Parse(well["radialTriggerZoneMesh"].Attributes[0].Value) * Constants.CentimeterToMeter,
                                        well["radialTriggerZoneMesh"].Attributes[1].Value,
                                        new Pillar(float.Parse(well["pillar"].Attributes[0].Value) * Constants.CentimeterToMeter, well["pillar"].Attributes[1].Value)));
            }
        }
    }

    /// <summary>
	/// Parses the XmlNodes that correspond to vertices and populates a Vector3 list
	/// Each of the elements in the list is a vertex
	/// </summary>
	/// <param name="xmlNode">vertices XmlNode</param>
	/// <param name="vertices">List to be populated</param>
    private void SetVertices(XmlNode xmlNode, List<Vector3> vertices) {
        foreach (XmlNode vertex in xmlNode)
            vertices.Add(new Vector3(float.Parse(vertex.Attributes[0].Value) * Constants.CentimeterToMeter, 0, float.Parse(vertex.Attributes[1].Value) * Constants.CentimeterToMeter));
    }

    /// <summary>
	/// Creates Dispenser objects of different types depeneding on dispenser type specified in the track file
	/// </summary>
	/// <param name="xmlNode">XmlNode containing dispenser data</param>
    private void SetDispensers(XmlNode xmlNode) {
        foreach (XmlNode dispenserNode in xmlNode) {
            if (dispenserNode.Name.Equals("audiodispenser")) {
                XmlNode audioDispenserSound = dispenserNode["sound"];
                Dispenser audioDispenser = new AudioDispenser(audioDispenserSound.Attributes[0].Value, audioDispenserSound.Attributes[1].Value,
                                                                float.Parse(audioDispenserSound.Attributes[2].Value),
                                                                float.Parse(audioDispenserSound.Attributes[3].Value) * Constants.CentimeterToMeter,
                                                                float.Parse(audioDispenserSound.Attributes[4].Value) * Constants.CentimeterToMeter);
                dispensers.Add(audioDispenser);
            }

            else if (dispenserNode.Name.Equals("someOtherDispenser")) { /* future TODO */}
        }
    }

    #endregion
}
