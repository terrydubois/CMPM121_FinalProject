/*
Following tutorial: https://www.youtube.com/watch?v=dycHQFEz8VI
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class GenerateTerrain : MonoBehaviour
    {

        void Start()
        {
            int heightScale = 5;
            float detailScale = 5.0f;

            Mesh mesh = this.GetComponent<MeshFilter>().mesh;
            Vector3[] verts = mesh.vertices;
            for (int i = 0; i < verts.Length; i++) {
                verts[i].y = Mathf.PerlinNoise((verts[i].x + this.transform.position.x) / detailScale,
                                                (verts[i].z + this.transform.position.z) / detailScale) * heightScale;
            }

            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            this.gameObject.AddComponent<MeshCollider>();
        }

        void Update()
        {
            
        }
    }
}