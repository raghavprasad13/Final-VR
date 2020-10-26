using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System;

public class TrackFileData {
    #region Constants

    const float CentimeterToMeter = 0.01f;
    const float MillisecondToSecond = 0.001f;

    #endregion

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

    #region Constructor(s)

    public TrackFileData() {
        boundaryVertices = new List<Vector3>();
        liveZoneVertices = new List<Vector3>();
        groundPolygonVertices = new List<Vector3>();
        dispensers = new List<Dispenser>();
        avatars = new List<RatAvatar>();
        planes = new List<Plane>();
        wells = new List<Well>();

        XmlReaderSettings readerSettings = new XmlReaderSettings();
        readerSettings.IgnoreComments = true;
        XmlReader xmlReader = XmlReader.Create(Path.Combine(Application.streamingAssetsPath, "random_5.track.xml"), readerSettings);

        XmlDocument trackFile = new XmlDocument();
        trackFile.Load(xmlReader);

        foreach (XmlNode xmlNode in trackFile.DocumentElement) {
            if (xmlNode.Name.Equals("boundary"))
                setVertices(xmlNode, boundaryVertices);
            else if (xmlNode.Name.Equals("livezone"))
                setVertices(xmlNode, liveZoneVertices);
            else if (xmlNode.Name.Equals("groundPolygon")) {
                groundPolygonMaterialName = xmlNode.Attributes[0].Value;
                setVertices(xmlNode, groundPolygonVertices);
            }
            else if (xmlNode.Name.Equals("bgcolor")) { /* do nothing for now */ }
            else if (xmlNode.Name.Equals("dispensers"))
                setDispensers(xmlNode);
            else if (xmlNode.Name.Equals("positions"))
                setPositions(xmlNode);
        }
    }

    #endregion

    #region Methods

    private void setPositions(XmlNode positionsNode) {
        XmlNode firstChildNode;
        foreach (XmlNode positionNode in positionsNode) {
            firstChildNode = positionNode.FirstChild;

            if (firstChildNode.Name.Equals("avatar"))
                avatars.Add(new RatAvatar(float.Parse(positionNode.Attributes[0].Value) * CentimeterToMeter,
                                            float.Parse(positionNode.Attributes[1].Value) * CentimeterToMeter,
                                            float.Parse(firstChildNode.Attributes[0].Value) * CentimeterToMeter,
                                            new Vector2(float.Parse(firstChildNode["direction"].Attributes[0].Value),
                                                        float.Parse(firstChildNode["direction"].Attributes[1].Value))));

            else if (firstChildNode.Name.Equals("plane"))
                planes.Add(new Plane(float.Parse(positionNode.Attributes[0].Value) * CentimeterToMeter,
                                        float.Parse(positionNode.Attributes[1].Value) * CentimeterToMeter,
                                        float.Parse(firstChildNode.Attributes[1].Value) * CentimeterToMeter,
                                        firstChildNode.Attributes[0].Value,
                                        firstChildNode.Attributes[2].Value,
                                        new Vector3(float.Parse(firstChildNode["facing"].Attributes[0].Value),
                                                    float.Parse(firstChildNode["facing"].Attributes[1].Value),
                                                    float.Parse(firstChildNode["facing"].Attributes[2].Value)),
                                        new Vector3(float.Parse(firstChildNode["scale"].Attributes[0].Value) * CentimeterToMeter,
                                                    float.Parse(firstChildNode["scale"].Attributes[1].Value) * CentimeterToMeter,
                                                    float.Parse(firstChildNode["scale"].Attributes[2].Value) * CentimeterToMeter)));

            else if (firstChildNode.Name.Equals("well")) {
                foreach(XmlNode well in positionNode)
                    wells.Add(new Well(float.Parse(positionNode.Attributes[0].Value) * CentimeterToMeter,
                                        float.Parse(positionNode.Attributes[1].Value) * CentimeterToMeter, well.Attributes[0].Value,
                                        float.Parse(well.Attributes[1].Value), int.Parse(well.Attributes[2].Value),
                                        float.Parse(well.Attributes[3].Value) * MillisecondToSecond,
                                        float.Parse(well.Attributes[4].Value) * MillisecondToSecond,
                                        float.Parse(well.Attributes[5].Value) * MillisecondToSecond,
                                        well.Attributes[6].Value.Equals("true"), float.Parse(well.Attributes[7].Value) * CentimeterToMeter,
                                        float.Parse(well.Attributes[8].Value) * CentimeterToMeter, float.Parse(well.Attributes[9].Value) * CentimeterToMeter,
                                        float.Parse(well.Attributes[10].Value) * CentimeterToMeter,
                                        float.Parse(well["radialboundary"].Attributes[0].Value) * CentimeterToMeter,
                                        float.Parse(well["radialTriggerZoneMesh"].Attributes[0].Value) * CentimeterToMeter,
                                        well["radialTriggerZoneMesh"].Attributes[1].Value,
                                        new Pillar(float.Parse(well["pillar"].Attributes[0].Value) * CentimeterToMeter, well["pillar"].Attributes[1].Value)));
            }
        }
    }

    private void setVertices(XmlNode xmlNode, List<Vector3> vertices) {
        foreach (XmlNode vertex in xmlNode)
            vertices.Add(new Vector3(float.Parse(vertex.Attributes[0].Value) * CentimeterToMeter, 0, float.Parse(vertex.Attributes[1].Value) * CentimeterToMeter    ));
    }

    private void setDispensers(XmlNode xmlNode) {
        foreach(XmlNode dispenserNode in xmlNode) {
            if (dispenserNode.Name.Equals("audiodispenser")) {
                XmlNode audioDispenserSound = dispenserNode["sound"];
                Dispenser audioDispenser = new AudioDispenser(audioDispenserSound.Attributes[0].Value, audioDispenserSound.Attributes[1].Value,
                                                                float.Parse(audioDispenserSound.Attributes[2].Value),
                                                                float.Parse(audioDispenserSound.Attributes[3].Value) * CentimeterToMeter,
                                                                float.Parse(audioDispenserSound.Attributes[4].Value) * CentimeterToMeter);
                dispensers.Add(audioDispenser);
            }

            else if(dispenserNode.Name.Equals("someOtherDispenser")) { /* future TODO */}
        }
    }

    #endregion
}
