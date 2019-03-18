using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrMovement : MonoBehaviour
    {
        public GameObject cameraObj;

        public float moveSpeed;
        public float jumpSpeed;
        private float rot = 0;
        public float rotSpeed = 80;
        public float gravity = 4;

        public bool selected = false;

        public bool autoRun = false;

        Vector3 moveDir = Vector3.zero;

        CharacterController controller;
        Animator animator;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            //animation = GetComponent<Animation>();
        }

        void Update()
        {
            if (cameraObj.GetComponent<ScrCamera>().introScreen) {

                //transform.localScale = Vector3.zero;
                cameraObj.transform.localScale = Vector3.one;
            }
            else {

                //transform.localScale = Vector3.one;
                cameraObj.transform.localScale = Vector3.zero;

                if (Input.GetKeyDown(KeyCode.X)) {
                    autoRun = !autoRun;
                    if (!autoRun) {
                        moveDir.x = 0;
                        moveDir.z = 0;
                        animator.SetInteger("animation", 0);
                    }
                }

                if (selected && !Input.GetKey(KeyCode.E) && controller.isGrounded) {
                    // walk forward
                    if (Input.GetKey(KeyCode.W) || autoRun) {
                        animator.SetInteger("animation", 1);
                        moveDir = new Vector3(0, 0, 1);
                        moveDir.z = 1;
                        moveDir.z *= moveSpeed;
                        
                        // run if player is holding X
                        if (Input.GetKey(KeyCode.LeftShift) || autoRun) {
                            moveDir *= 2;
                            animator.SetInteger("animation", 2);
                        }

                        moveDir = transform.TransformDirection(moveDir);
                    }
                    
                    // walk backwards
                    if (Input.GetKey(KeyCode.S)) {
                        animator.SetInteger("animation", 1);
                        moveDir = new Vector3(0, 0, -0.5f);
                        moveDir *= moveSpeed;
                        moveDir = transform.TransformDirection(moveDir);
                    }

                    // rotate if pressing left/right
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                        float runMultiply = 1;
                        if (Input.GetKey(KeyCode.LeftShift) || autoRun) {
                            runMultiply = 1.5f;
                        }
                        else {
                            animator.SetInteger("animation", 1);
                        }
                        rot += Input.GetAxis("Horizontal") * rotSpeed * runMultiply * Time.deltaTime;
                        transform.eulerAngles = new Vector3(0, rot, 0);
                    }


                    // jump
                    if (Input.GetKey(KeyCode.Space)) {
                        moveDir.y = jumpSpeed;
                        animator.SetInteger("animation", 3);
                    }
                }


                
                if (controller.isGrounded && animator.GetInteger("animation") == 3 && !Input.GetKey(KeyCode.Space)) {
                    animator.SetInteger("animation", 0);
                }
                
                
                if (Input.GetKey(KeyCode.E) && controller.isGrounded) {
                    moveDir = Vector3.zero;
                    animator.SetInteger("animation", 4);
                }

                // return to standstill
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)
                || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.E) || !selected) {
                    animator.SetInteger("animation", 0);
                    moveDir = Vector3.zero;
                }

            }
            
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}