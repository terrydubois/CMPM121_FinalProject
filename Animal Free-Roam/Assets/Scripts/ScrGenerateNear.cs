using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrGenerateNear : MonoBehaviour
{
    public string tag;
    public int amount;
    public GameObject[] prefabs;

    private GameObject character;
    private GameObject[] objects;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        objects = GameObject.FindGameObjectsWithTag(tag);
        if (objects.Length < amount) {
            int prefabIndex = Random.Range(0, prefabs.Length);
            GameObject newObj = (GameObject) Instantiate(prefabs[prefabIndex], getObjPos(), Quaternion.identity);
        }
    }

    Vector3 getObjPos() {

        Vector3 charPos = character.transform.position;

        float plusX = Random.Range(120.0f, 180.0f);
        if (Random.Range(0, 10) >= 5) {
            plusX *= -1;
        }

        float plusZ = Random.Range(120.0f, 180.0f);
        if (Random.Range(0, 10) >= 5) {
            plusZ *= -1;
        }

        Vector3 newPos = new Vector3(
            charPos.x + plusX,
            3,
            charPos.z + plusZ
        );

        return newPos;
    }
}
