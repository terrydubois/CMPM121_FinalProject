using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrFruit : MonoBehaviour
    {
        private GameObject character;
        private GameObject camObj;

        void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player");
            camObj = GameObject.FindGameObjectWithTag("MainCamera");
        }

        void Update()
        {
            float distToChar = Vector3.Distance(transform.position, character.transform.position);

            if (distToChar > 450) {
                Destroy(gameObject);
            }
            else if (distToChar < 1.0f) {
                if (Input.GetKey(KeyCode.E)) {
                    camObj.GetComponent<ScrCamera>().score++;
                    Destroy(gameObject);
                }
            }

            if (transform.position.y < 0.5f) {
                Destroy(gameObject);
            }


        }
    }
}