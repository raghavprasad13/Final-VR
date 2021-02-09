using System.Collections.Generic;
using UnityEngine;


namespace Builder {
    /// <summary>
	/// Builds individual components of a track and instantiates them as GameObjects
	/// </summary>
	public class GameObjectBuilder : MonoBehaviour {

        /// <summary>
		/// This method procedurally creates the meshes for the walls
		/// </summary>
		/// <param name="planes"> A list of Plane objects </param>
		public static void Walls(List<Plane> planes, GameObject parent = null) {
            //print("planes.Count: " + planes.Count);
			foreach(Plane plane in planes) {
				GameObject planeGameObject = new GameObject(plane.Name);
                if(parent != null)
				    planeGameObject.transform.parent = parent.transform;

				planeGameObject.transform.position = new Vector3(plane.Center.x, plane.Height, plane.Center.z);
				AddMeshComponents(planeGameObject);

				CreateMesh(planeGameObject, GetPlaneVertices(plane) /*, plane.Facing */);
				planeGameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
				//Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
				////tex.SetPixels(Resources.Load<Texture2D>("wall_images/Tank_North_Marked.png").GetPixels());
				////tex.Apply();
				//planeGameObject.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("wall_images/Tank_North_Marked.png");
				planeGameObject.AddComponent<Rigidbody>();
				planeGameObject.GetComponent<Rigidbody>().isKinematic = true;
				planeGameObject.AddComponent<MeshCollider>();
				//planeGameObject.GetComponent<MeshCollider>().convex = true;
				planeGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
		}

        public static void Planes(List<Plane> planes, GameObject parent = null) {
            GameObject planePrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/Plane");
            //Quaternion rotation;
            foreach(Plane plane in planes) {
                //if(plane.Facing.z == -1)
                //    rotation = Quaternion.AngleAxis()
                GameObject planeGameObject = Instantiate(planePrefab, new Vector3(plane.Center.x, plane.Height, plane.Center.z), Quaternion.identity);

                if (parent != null)
                    planeGameObject.transform.parent = parent.transform;

                planeGameObject.transform.localScale = plane.Scale / 10f;

                //planeGameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1F), Random.Range(0, 1F), Random.Range(0, 1F));

                string materialNameNoExt = plane.Material.Split('.')[0];

				planeGameObject.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("Textures/" + materialNameNoExt);

                if (plane.Facing.z == -1)
                    planeGameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.right);
                else if (plane.Facing.z == 1)
                    planeGameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
                else if (plane.Facing.x == -1)
                    planeGameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                else if (plane.Facing.x == 1)
                    planeGameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);


                planeGameObject.name = plane.Name;
			}
		}

        /// <summary>
		/// Depending on the direction the plane is supposed to be facing, 4 vertices are assigned values
		/// using the center, height and length information of the plane
		/// </summary>
		/// <param name="plane">The plane whose vertices need to be computed</param>
		/// <returns>A Vector3 array of 4 elements, each element representing one vertex of the rectangular plane</returns>
        private static Vector3[] GetPlaneVertices(Plane plane) {
            Vector3[] vertices = new Vector3[4];
            float planeLength, planeHeight;

            if (plane.Facing.x == 1) {
                planeHeight = 2 * plane.Scale.x;
                planeLength = 2 * plane.Scale.z;

                vertices[0] = plane.Center + new Vector3(0, -planeHeight / 2, -planeLength / 2);
                vertices[1] = plane.Center + new Vector3(0, planeHeight / 2, -planeLength / 2);
                vertices[2] = plane.Center + new Vector3(0, planeHeight / 2, planeLength / 2);
                vertices[3] = plane.Center + new Vector3(0, -planeHeight / 2, planeLength / 2);
            }

            if (plane.Facing.x == -1) {
                planeHeight = 2 * plane.Scale.x;
                planeLength = 2 * plane.Scale.z;

                vertices[0] = plane.Center + new Vector3(0, -planeHeight / 2, planeLength / 2);
                vertices[1] = plane.Center + new Vector3(0, planeHeight / 2, planeLength / 2);
                vertices[2] = plane.Center + new Vector3(0, planeHeight / 2, -planeLength / 2);
                vertices[3] = plane.Center + new Vector3(0, -planeHeight / 2, -planeLength / 2);
            }

            if (plane.Facing.z == 1) {
                planeHeight = 2 * plane.Scale.z;
                planeLength = 2 * plane.Scale.x;

                vertices[0] = plane.Center + new Vector3(planeLength / 2, -planeHeight / 2, 0);
                vertices[1] = plane.Center + new Vector3(planeLength / 2, planeHeight / 2, 0);
                vertices[2] = plane.Center + new Vector3(-planeLength / 2, planeHeight / 2, 0);
                vertices[3] = plane.Center + new Vector3(-planeLength / 2, -planeHeight / 2, 0);
            }

            if (plane.Facing.z == -1) {
                planeHeight = 2 * plane.Scale.z;
                planeLength = 2 * plane.Scale.x;

                vertices[0] = plane.Center + new Vector3(-planeLength / 2, -planeHeight / 2, 0);
                vertices[1] = plane.Center + new Vector3(-planeLength / 2, planeHeight / 2, 0);
                vertices[2] = plane.Center + new Vector3(planeLength / 2, planeHeight / 2, 0);
                vertices[3] = plane.Center + new Vector3(planeLength / 2, -planeHeight / 2, 0);
            }

            if (plane.Facing.y == -1) {
                planeHeight = 2 * plane.Scale.z;
                planeLength = 2 * plane.Scale.x;

                vertices[0] = plane.Center + new Vector3(-planeLength / 2, 0, -planeHeight / 2);
                vertices[1] = plane.Center + new Vector3(-planeLength / 2, 0, planeHeight / 2);
                vertices[2] = plane.Center + new Vector3(planeLength / 2, 0, planeHeight / 2);
                vertices[3] = plane.Center + new Vector3(planeLength / 2, 0, -planeHeight / 2);
            }

            return vertices;
        }

        private static void AddMeshComponents(GameObject gObject) {
            gObject.AddComponent<MeshFilter>();
            gObject.AddComponent<MeshRenderer>();
        }

        /// <summary>
		/// Creates a mesh for the GameObject from the vertices array
		/// This forms the crux of the procedural mesh generation
		/// </summary>
		/// <param name="gObject">GameObject whose mesh needs to be created</param>
		/// <param name="vertices">Vertices of the shape</param>
        private static void CreateMesh(GameObject gObject, Vector3[] vertices /*, Vector3 facing */) {
            Mesh mesh = gObject.GetComponent<MeshFilter>().mesh;
            mesh.vertices = vertices;

            List<int> triangleVertices = new List<int>();   // Procedural mesh generation requires an array of triangles. Read more about this here: TODO: insert URL for documentation here

            int firstVertex = 0;
            int secondVertex = 1;
            int thirdVertex = 2;

            for (int i = 0; thirdVertex < vertices.Length; i++) {
                triangleVertices.Add(firstVertex);
                triangleVertices.Add(secondVertex);
                triangleVertices.Add(thirdVertex);
                secondVertex++;
                thirdVertex++;
            }

            mesh.triangles = triangleVertices.ToArray();

            //      Vector2[] vertexUVs = new Vector2[vertices.Length];
            //      for(int i = 0; i < vertexUVs.Length; i++) {
            //          if (Math.Abs(facing.z) == 1)
            //              vertexUVs[i] = new Vector2(vertices[i].x, vertices[i].y);
            //}

            //mesh.uv = CalculateUVs(triangles, 1.0f, facing);
            mesh.RecalculateNormals();

            // TODO: Implement UVs to properly display texture material

            //Vector2[] meshUVs = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1),
            //                                new Vector2(1, 0), new Vector2(1, 1)};

            //mesh.uv = meshUVs;

            //return mesh;
        }

        public static Vector2[] CalculateUVs(Vector3[] v/*vertices*/, float scale, Vector3 facing) {
            var uvs = new Vector2[v.Length];

            for (int i = 0; i < uvs.Length; i += 3) {
                int i0 = i;
                int i1 = i + 1;
                int i2 = i + 2;

                Vector3 v0 = v[i0];
                Vector3 v1 = v[i1];
                Vector3 v2 = v[i2];

                // Vector3 side1 = v1 - v0;
                // Vector3 side2 = v2 - v0;
                // var direction = Vector3.Cross(side1, side2);
                // var facing = FacingDirection(direction);

                if (facing.z == -1 || facing.z == 1) {
                    uvs[i0] = ScaledUV(v0.x, v0.y, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.y, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.y, scale);
                }
                else if (facing.y == -1) {
                    uvs[i0] = ScaledUV(v0.x, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.z, scale);
                }

                else if (facing.x == -1 || facing.x == 1) {
                    uvs[i0] = ScaledUV(v0.y, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.y, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.y, v2.z, scale);
                }
            }

            return uvs;
        }

        private static Vector2 ScaledUV(float uv1, float uv2, float scale) {
            return new Vector2(uv1 / scale, uv2 / scale);
        }

        /// <summary>
		/// Creates the surface on which the rat is going to be walking
		/// </summary>
		/// <param name="groundPolygonVertices">Vertices of the polygonal plane surface</param>
		/// <param name="parent">The parent GameObject of which the ground GameObject will be a child</param>
        public static void Ground(GroundPolygon groundPolygon, GameObject parent = null) {
			GameObject ground = new GameObject("Ground");

            if(parent != null)
                ground.transform.parent = parent.transform;

			ground.transform.position = new Vector3(0, /* -0.0508f */ 0, 0);

			AddMeshComponents(ground);

            if (IsAnticlockwise(groundPolygon.Vertices)) {
                groundPolygon.Vertices.Reverse();
            }

            CreateMesh(ground, groundPolygon.Vertices.ToArray());
            ground.GetComponent<MeshRenderer>().material.mainTexture = Texture2D.blackTexture; // Resources.Load("Materials/TabletopMaterial") as Material;
			ground.AddComponent<Rigidbody>();
			ground.GetComponent<Rigidbody>().isKinematic = true;
			ground.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ground.AddComponent<MeshCollider>();
			ground.GetComponent<MeshCollider>().convex = true;
		}

        /// <summary>
		/// The rat must not walk off the surface. Thus it must be bounded
		/// Here, the booundary is made up using short invisible walls
		/// </summary>
		/// <param name="boundaryVertices">The vertices that define the boundary</param>
		/// <param name="parent">The parent GameObject of which the boundary GameObject will be a child<</param>
        public static void Boundary(List<Vector3> boundaryVertices, GameObject parent = null) {
            GameObject boundaries = new GameObject("Boundaries");

            if(parent != null)
                boundaries.transform.parent = parent.transform;

			GameObject boundaryGeneratorPrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/Cube");
            //print(boundaryGeneratorPrefab.name);

            for(int i = 1; i <= boundaryVertices.Count; i++) {
                var length = (boundaryVertices[i % boundaryVertices.Count] - boundaryVertices[(i - 1) % boundaryVertices.Count]).magnitude;
                var center = (boundaryVertices[i % boundaryVertices.Count] + boundaryVertices[(i - 1) % boundaryVertices.Count]) / 2;

                Vector3 correctDirection = Vector3.zero - center;

                var boundaryObj = Instantiate(boundaryGeneratorPrefab, new Vector3(center.x, 0.3f, center.z), Quaternion.identity);
                boundaryObj.transform.parent = boundaries.transform;

                boundaryObj.transform.localScale = new Vector3(length + 0.1f, 0.6f, 1e-4f);
                Vector3 facing = boundaryObj.transform.forward;

                float rotationAngle = Vector3.Angle(facing, correctDirection);

                boundaryObj.transform.eulerAngles = new Vector3(0, rotationAngle, 0);

				if (Vector3.Angle(correctDirection, boundaryObj.transform.forward) == 90)
					boundaryObj.transform.eulerAngles = new Vector3(0, rotationAngle + 90, 0);

				boundaryObj.AddComponent<Rigidbody>();
                Rigidbody boundaryRigidbody = boundaryObj.GetComponent<Rigidbody>();

                boundaryRigidbody.isKinematic = true;
                boundaryRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                boundaryRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        /// <summary>
		/// Instantiates each avatar in the correct position and facing the correct direction
		/// </summary>
		/// <param name="ratAvatars">List of avatars</param>
        public static void Avatar(RatAvatar avatar, GameObject parent = null) {
            GameObject avatarPrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/New Avatar");
            GameObject avatarGameObject = Instantiate(avatarPrefab, new Vector3(avatar.Position.x, avatar.Height + (1 * 0.0508f), avatar.Position.y), Quaternion.identity); // TODO: Modify this line to account for facing direction given in track file
            avatarGameObject.name = "Avatar";
            //avatarGameObject.tag = "Avatar";
            avatarGameObject.layer = 11;    // Layer 11 is the Avatar Layer
		}

        public static void Wells(List<Well> wells, GameObject parent = null) {
            GameObject Wells = new GameObject("Wells");
            if (parent != null)
                Wells.transform.parent = parent.transform;

            Wells.transform.position = Vector3.zero;

            List<Well> wellsAssigned = new List<Well>();

            Vector3 wellPosition;
            System.Random random = new System.Random();

            GameObject circularPlanePrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/CircularPlane");

			foreach (Well well in wells) {
                if(well.GetType().ToString().Equals("RandomWell")) {

                    while (true) {
                        float wellPositionX = (float)random.NextDouble() * (well.Q1Max - well.Q1Min) + well.Q1Min;
                        float wellPositionZ = (float)random.NextDouble() * (well.Q2Max - well.Q2Min) + well.Q2Min;
                        
                        wellPosition = new Vector3(wellPositionX, 1e-4f, wellPositionZ);
                        well.Position = new Vector2(wellPositionX, wellPositionZ);

                        if (!IsOverlapping(wellsAssigned, well)) {
                            wellsAssigned.Add(well);
                            break;
                        }
                    }
				}

                else
                    wellPosition = new Vector3(well.Position.x, 1e-4f, well.Position.y);

                GameObject wellGameObject = Instantiate(circularPlanePrefab, wellPosition, Quaternion.identity);
                //print("RADIAL BOUNDARY RADIUS: " + well.RadialBoundaryRadius);
				wellGameObject.transform.localScale = new Vector3(well.RadialBoundaryRadius, 1e-6f, well.RadialBoundaryRadius);

                string materialNameNoExt = well.RadialTriggerZoneMeshMaterial.Split('.')[0];
                wellGameObject.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("Textures/" + materialNameNoExt);

                if (well.Level == 0)
                    wellGameObject.GetComponent<Renderer>().enabled = false;

				wellGameObject.name = well.Name;
                wellGameObject.transform.parent = Wells.transform;
            }
		}

        public static void Dispensers(List<Dispenser> dispensers, GameObject parent = null) {
            /* TODO */
		}

        public static bool IsOverlapping(List<Well> wells, Well well) {
            foreach(Well assignedWell in wells) {
                if ((assignedWell.Position - well.Position).magnitude < (assignedWell.RadialBoundaryRadius + well.RadialBoundaryRadius))
                    return true;
			}
            return false;
		}

        public static void OccupationZones(List<OccupationZone> occupationZones, GameObject parent = null) {
            GameObject OccupationZones = new GameObject("Occupation Zones");
            if (parent != null)
                OccupationZones.transform.parent = parent.transform;

            OccupationZones.transform.position = Vector3.zero;

            GameObject circularPlanePrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/CircularPlane");

			foreach (OccupationZone occupationZone in occupationZones) {
				print("OccupationZone: " + occupationZone.Name + "\t" + occupationZone.IsRadialBoundary);

				GameObject occupationZoneGameObject;
				if (occupationZone.IsRadialBoundary) {
					occupationZoneGameObject = Instantiate(circularPlanePrefab, occupationZone.Position, Quaternion.identity);
					occupationZoneGameObject.transform.localScale = new Vector3(occupationZone.RadialBoundaryRadius, 1e-2f, occupationZone.RadialBoundaryRadius);
				}

				else {
					occupationZoneGameObject = new GameObject();

					occupationZoneGameObject.transform.position = occupationZone.Position;

					AddMeshComponents(occupationZoneGameObject);
					CreateMesh(occupationZoneGameObject, occupationZone.PolygonBoundaryVertices.ToArray());
					occupationZoneGameObject.GetComponent<MeshRenderer>().material.mainTexture = Texture2D.blackTexture; // Resources.Load("Materials/TabletopMaterial") as Material;
					//occupationZoneGameObject.AddComponent<Rigidbody>();
					//occupationZoneGameObject.GetComponent<Rigidbody>().isKinematic = true;
					occupationZoneGameObject.AddComponent<MeshCollider>();
					occupationZoneGameObject.GetComponent<MeshCollider>().convex = true;
                    occupationZoneGameObject.AddComponent<Action>();
				}



                occupationZoneGameObject.name = occupationZone.Name;
                occupationZoneGameObject.GetComponent<Action>().occupationZone = occupationZone;
				occupationZoneGameObject.GetComponent<Renderer>().enabled = false;
                occupationZoneGameObject.GetComponent<MeshCollider>().isTrigger = occupationZone.IsActive;
				occupationZoneGameObject.transform.parent = OccupationZones.transform;
			}
		}

        public static bool IsAnticlockwise(List<Vector3> vertices) {
            float sum = 0;
            for(int i = 0; i < vertices.Count; i++) {
                int firstVertexIndex = (i + 1) < vertices.Count ? (i + 1) : 0;
                float edge = (vertices[firstVertexIndex].x - vertices[i].x) * (vertices[firstVertexIndex].z + vertices[i].z);
                sum += edge;
            }

            if (sum < 0) {
                print("Is ANTICLOCKWISE");
                return true;
            }
            return false;
		}

    }
}
