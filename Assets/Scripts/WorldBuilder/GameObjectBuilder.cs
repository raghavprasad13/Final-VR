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
            print("planes.Count: " + planes.Count);
			foreach(Plane plane in planes) {
				GameObject planeGameObject = new GameObject(plane.Name);
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

			print("PLANES BUILT");
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
            ground.transform.parent = parent.transform;

			ground.transform.position = new Vector3(0, /* -0.0508f */ 0, 0);

			AddMeshComponents(ground);
			CreateMesh(ground, groundPolygon.Vertices.ToArray());
            ground.GetComponent<MeshRenderer>().material.mainTexture = Texture2D.blackTexture; // Resources.Load("Materials/TabletopMaterial") as Material;
			ground.AddComponent<Rigidbody>();
			ground.GetComponent<Rigidbody>().isKinematic = true;
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
            boundaries.transform.parent = parent.transform;

			GameObject boundaryGeneratorPrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/Cube");
            print(boundaryGeneratorPrefab.name);

            for(int i = 1; i <= boundaryVertices.Count; i++) {
                var length = (boundaryVertices[i % boundaryVertices.Count] - boundaryVertices[(i - 1) % boundaryVertices.Count]).magnitude;
                var center = (boundaryVertices[i % boundaryVertices.Count] + boundaryVertices[(i - 1) % boundaryVertices.Count]) / 2;

                Vector3 correctDirection = Vector3.zero - center;

                var boundaryObj = Instantiate(boundaryGeneratorPrefab, new Vector3(center.x, 0.3f, center.z), Quaternion.identity);
                boundaryObj.transform.parent = boundaries.transform;

                boundaryObj.transform.localScale = new Vector3(length + 0.1f, 0.6f, 0.05f);
                Vector3 facing = boundaryObj.transform.forward;

                float rotationAngle = Vector3.Angle(facing, correctDirection);

                boundaryObj.transform.eulerAngles = new Vector3(0, rotationAngle, 0);

				if (Vector3.Angle(correctDirection, boundaryObj.transform.forward) == 90)
					boundaryObj.transform.eulerAngles = new Vector3(0, rotationAngle + 90, 0);
            }
        }

        /// <summary>
		/// Instantiates each avatar in the correct position and facing the correct direction
		/// </summary>
		/// <param name="ratAvatars">List of avatars</param>
        public static void Avatar(RatAvatar avatar, GameObject parent = null) {
            GameObject avatarPrefab = Resources.Load<GameObject>("3D_Objects/Prefabs/Participant");
            GameObject avatarGameObject = Instantiate(avatarPrefab, new Vector3(avatar.Position.x, avatar.Height + 0.0508f, avatar.Position.y), Quaternion.identity); // TODO: Modify this line to account for facing direction given in track file
            avatarGameObject.name = "Avatar";
		}

        public static void Wells(List<Well> wells, GameObject parent = null) {
            /* TODO */
		}

        public static void Dispensers(List<Dispenser> dispensers, GameObject parent = null) {
            /* TODO */
		}


    }
}
