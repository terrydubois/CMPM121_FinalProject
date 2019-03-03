using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrCamera : MonoBehaviour
    {
        
        static int characterAmount = 1;
        public GameObject[] characters = new GameObject[characterAmount];
        private int currentCharacter = 0;

        /*
        // https://answers.unity.com/questions/600577/camera-rotation-around-player-while-following.html
        public float turnSpeed = 4.0f;
        public Transform playerTransform;
        private Vector3 offset;

        private float hOffset;
        private float vOffset;
        
        void Start()
        {
            hOffset = 0;//new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 5.0f);
            vOffset = 0;//new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        }
        */
        //void Update()
        //{
            //playerTransform = characters[currentCharacter].transform;
            
            
            //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            /*
            transform.position = playerTransform.position + offset;
            transform.LookAt(playerTransform.position);
            */
            //hOffset +=  Input.GetAxis("HorizontalCam") * Time.deltaTime;
            //vOffset +=  Input.GetAxis("VerticalCam") * Time.deltaTime;
            //hOffset = Quaternion.AngleAxis(Input.GetAxis("HorizontalCam") * turnSpeed, Vector3.up) * hOffset;

            //transform.position = playerTransform.position + hOffset;
            //transform.LookAt(playerTransform.position);


            /*
            Vector3 relativePos = (playerTransform.position + new Vector3(0, 0, 0)) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion currentRot = transform.localRotation;

            transform.localRotation = Quaternion.Slerp(currentRot, rotation, Time.deltaTime);
            transform.Translate(0, 0, 3 * Time.deltaTime);

            transform.LookAt(playerTransform.position);
            

            if (Input.GetKey(KeyCode.W)) {
                vOffset -= 0.01f;
            }
            else if (Input.GetKey(KeyCode.S)) {
                vOffset += 0.01f;
            }
            if (Input.GetKey(KeyCode.A)) {
                hOffset -= 0.01f;
            }
            else if (Input.GetKey(KeyCode.D)) {
                hOffset += 0.01f;
            }

            Vector3 relativePos = (playerTransform.position + new Vector3(hOffset, 0, 0)) - transform.position;
            transform.position = playerTransform.position + new Vector3(hOffset, 1, vOffset);
            transform.LookAt(playerTransform.position);
            */
        //}
        

        // Following tutorial: https://www.youtube.com/watch?v=Ta7v27yySKs

        public Transform lookAt;
        public Transform camTransform;

        private Camera cam;

        private float distance = 1.0f;
        private float currentX = 0.0f;
        private float currentY = 0.0f;
        private float sensX = 4.0f;
        private float sensY = 1.0f;

        private const float Y_ANGLE_MIN = 20.0f;
        private const float Y_ANGLE_MAX = 60.0f;

        private void Start() {
            camTransform = transform;
            cam = Camera.main;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Z)) {
                if (currentCharacter < characterAmount - 1) {
                    currentCharacter++;
                }
                else {
                    currentCharacter = 0;
                }
            }

            for (int i = 0; i < characterAmount; i++) {
                characters[i].GetComponent<ScrMovement>().selected = false;
            }
            characters[currentCharacter].GetComponent<ScrMovement>().selected = true;
            lookAt = characters[currentCharacter].transform;

            
            currentX += Input.GetAxis("Mouse X") * sensX;
            currentY += Input.GetAxis("Mouse Y") * -sensY;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }

        private void LateUpdate() {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rot * dir;
            camTransform.LookAt(lookAt.position);
        }

    }
}