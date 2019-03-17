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
        float distToChar = Vector3.Distance(transform.position, character.transform.position);

        if (distToChar > 70) {
            Destroy(gameObject);
        }
        else if (distToChar < 0.5f) {
            if (Input.GetKey(KeyCode.E)) {
                Destroy(gameObject);
            }
        }


    }
}
