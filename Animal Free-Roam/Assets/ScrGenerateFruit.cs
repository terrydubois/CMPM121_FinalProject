using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrGenerateFruit : MonoBehaviour
{
    public GameObject character;
    public int fruitAmount;
    public GameObject[] fruitPrefabs;
    private GameObject[] fruitObjects;

    void Start()
    {

    }

    void Update()
    {
        fruitObjects = GameObject.FindGameObjectsWithTag("Fruit");
        if (fruitObjects.Length < fruitAmount) {
            
            int prefabIndex = Random.Range(0, fruitPrefabs.Length);
            GameObject newFruit = (GameObject) Instantiate(fruitPrefabs[prefabIndex], getFruitPos(), Quaternion.identity);
        }
    }

    Vector3 getFruitPos() {

        Vector3 charPos = character.transform.position;

        float plusX = Random.Range(30.0f, 50.0f);
        if (Random.Range(0, 10) >= 5) {
            plusX *= -1;
        }

        float plusZ = Random.Range(30.0f, 50.0f);
        if (Random.Range(0, 10) >= 5) {
            plusZ *= -1;
        }

        Vector3 newPos = new Vector3(
            charPos.x + plusX,
            charPos.y + 10,
            charPos.z + plusZ
        );

        return newPos;
    }
}
