using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrFruit : MonoBehaviour
{
    private GameObject character;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, character.transform.position) > 70) {
            Destroy(gameObject);
        }
    }
}
