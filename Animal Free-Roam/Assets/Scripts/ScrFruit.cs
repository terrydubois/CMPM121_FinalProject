using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrFruit : MonoBehaviour
    {
        private GameObject character;
        private GameObject camObj;

        private GameObject particleObj;
        private ParticleSystem ps;

        void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player");
            camObj = GameObject.FindGameObjectWithTag("MainCamera");
            particleObj = GameObject.FindGameObjectWithTag("EatingParticle");
            
            ps = particleObj.GetComponent<ParticleSystem>();
            var em = ps.emission;
            em.enabled = false;
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

                    if (particleObj) {
                        particleObj.transform.position = transform.position;
                    }
                    var em = ps.emission;
                    ps.Clear();
                    ps.Play();
                    em.enabled = true;

                    Destroy(gameObject);
                }
            }

            if (transform.position.y < 0.5f) {
                Destroy(gameObject);
            }
        }
    }
}