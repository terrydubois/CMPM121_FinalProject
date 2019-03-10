﻿/*
Following tutorial: https://www.youtube.com/watch?v=dycHQFEz8VI
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class GenerateTerrain : MonoBehaviour
    {
        List<GameObject> treeList = new List<GameObject>();

        void Start()
        {
            int heightScale = 10;
            float detailScale = 20.0f;
            
            Mesh mesh = this.GetComponent<MeshFilter>().mesh;
            Vector3[] verts = mesh.vertices;
            for (int i = 0; i < verts.Length; i++) {
                verts[i].y = Mathf.PerlinNoise((verts[i].x + this.transform.position.x) / detailScale,
                                                (verts[i].z + this.transform.position.z) / detailScale) * heightScale;


                if (verts[i].y > 7.5f// && Mathf.PerlinNoise((verts[i].x+5) / 10, (verts[i].z + 5) / 10) * 10 > 4.6
                && Random.Range(0, 20) > 18) {
                    GameObject newTree = ScrTreePoolCreate.getTree();

                    if (newTree != null) {
                        Vector3 treePos = new Vector3(
                            verts[i].x + this.transform.position.x,
                            verts[i].y - 1,
                            verts[i].z + this.transform.position.z
                        );
                        newTree.transform.position = treePos;
                        newTree.SetActive(true);
                        treeList.Add(newTree);
                    }
                }
            }

            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            this.gameObject.AddComponent<MeshCollider>();
        }

        void Update()
        {
            
        }

        void OnDestroy() {
            for (int i = 0; i < treeList.Count; i++) {
                if (treeList[i] != null) {
                    treeList[i].SetActive(false);
                }
            }
            
            treeList.Clear();
        }
    }
}