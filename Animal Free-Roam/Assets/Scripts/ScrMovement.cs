using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrMovement : MonoBehaviour
    {
        public float moveSpeed;
        public float jumpSpeed;
        private float rot = 0;
        public float rotSpeed = 80;
        public float gravity = 8;

        public bool selected = false;

        Vector3 moveDir = Vector3.zero;

        CharacterController controller;
        Animator animator;
        //Animation animation;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            //animation = GetComponent<Animation>();
        }

        void Update()
        {

            if (selected && controller.isGrounded && !Input.GetKey(KeyCode.E)) {
                // walk forward
                if (Input.GetKey(KeyCode.W)) {
                    animator.SetInteger("animation", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= moveSpeed;
                    
                    // run if player is holding X
                    if (Input.GetKey(KeyCode.LeftShift)) {
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
            
            if (Input.GetKey(KeyCode.E)) {
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

            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}