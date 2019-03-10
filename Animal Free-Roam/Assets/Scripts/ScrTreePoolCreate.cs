using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrTreePoolCreate : MonoBehaviour
{
    static int treeAmount = 300;
    public GameObject treePrefab;
    static GameObject[] trees;
    
    void Start()
    {
        trees = new GameObject[treeAmount];
        for (int i = 0; i < treeAmount; i++) {
            trees[i] = (GameObject) Instantiate(treePrefab, Vector3.zero, Quaternion.identity);
            trees[i].SetActive(false);
        }
    }

    static public GameObject getTree() {

        for (int i = 0; i < treeAmount; i++) {
            if (!trees[i].activeSelf) {
                return trees[i];
            }
        }
        return null;
    }

    void Update()
    {
        
    }
}
