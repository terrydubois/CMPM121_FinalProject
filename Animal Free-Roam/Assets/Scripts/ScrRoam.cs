using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrRoam : MonoBehaviour
    {
        private GameObject playerObj;
        private GameObject timeObj;

        public float moveSpeed;
        private float rot = 0;
        public float rotSpeed = 80;
        public float gravity = 6;

        public bool walking = true;
        public bool eating = true;
        public int rotatingDir = 0;

        public float maxDistToChar = 450;

        private float scale = 0;
        private float scaleDest = 6.0f;

        private float sleepTimePlus;
        private bool sleeping;

        Vector3 moveDir = Vector3.zero;

        CharacterController controller;
        Animator animator;

        void Start()
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");

            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            timeObj = GameObject.FindGameObjectWithTag("TimeControl");

            float waitTime = Random.Range(2.0f, 10.0f);
            
            InvokeRepeating("ChangeDir", waitTime / 2, waitTime);
            
            transform.localScale = Vector3.zero;
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

            sleepTimePlus = Random.Range(-0.13f, 0.13f);
            sleeping = false;
        }

        void Update()
        {
            if (scale < scaleDest) {
                scale += 0.4f;
            }
            else {
                scale = scaleDest;
            }
            transform.localScale = new Vector3(scale, scale, scale);

            if (controller.isGrounded) {
                // walk forward
                if (walking) {
                    animator.SetInteger("animation", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir.z = 1;
                    moveDir.z *= moveSpeed;

                    moveDir = transform.TransformDirection(moveDir);
                }
                
                // rotate
                if (rotatingDir != 0) {
                    float runMultiply = 1;
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        runMultiply = 1.5f;
                    }
                    else {
                        animator.SetInteger("animation", 1);
                    }
                    rot += Input.GetAxis("Horizontal") * rotSpeed * runMultiply * Time.deltaTime;
                    transform.eulerAngles = new Vector3(0, rot, 0);
                }
            }


            
            if (controller.isGrounded && animator.GetInteger("animation") == 3) {
                animator.SetInteger("animation", 0);
            }
            
            

            // return to standstill
            
            if (!walking) {
                animator.SetInteger("animation", 0);
                moveDir.x = 0;
                moveDir.z = 0;

                if (eating && controller.isGrounded) {
                    moveDir = Vector3.zero;
                    animator.SetInteger("animation", 4);
                }
            }
            


            if (transform.position.y < 1.6) {
                walking = true;
            }
            if (transform.position.y < 1) {
                Destroy(gameObject);
            }
            if (Vector3.Distance(transform.position, playerObj.GetComponent<Transform>().position) > maxDistToChar) {
                Destroy(gameObject);
            }

            // sleeping
            sleeping = false;
            float currentTime = timeObj.GetComponent<ScrTimeControl>().currentTime;
            if (currentTime < 0.13f + sleepTimePlus || currentTime > 0.93f + sleepTimePlus) {
                if (transform.position.y > 1.6f) {
                    sleeping = true;
                }
            }
            if (sleeping) {
                walking = false;
                animator.SetInteger("animation", 5);
            }
            

            
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }

        void newPosition()
        {
            Transform playerTransform = playerObj.GetComponent<Transform>();

            float newX = playerTransform.position.x;
            float newZ = playerTransform.position.z;

            if (Random.Range(0, 10) >= 5) {
                newX += Random.Range(30, 60);
            }
            else {
                newX -= Random.Range(30, 60);
            }
            if (Random.Range(0, 10) >= 5) {
                newZ += Random.Range(30, 60);
            }
            else {
                newZ -= Random.Range(30, 60);
            }
           
            transform.position = new Vector3(newX, 6.0f, newZ);
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }

        void ChangeDir()
        {
            if (sleeping) {
                return;
            }

            eating = !eating;

            if (Random.Range(0, 10) >= 5) {
                walking = !walking;

                if (walking) {
                    rotatingDir = 1;
                    if (Random.Range(0, 10) >= 5) {
                        rotatingDir *= -1;
                    }
                }
                else {
                    rotatingDir = 0;
                }
            }

        }
    }
}