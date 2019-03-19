using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun {
    public class ScrShowControls : MonoBehaviour
    {
        private GameObject camObj;
        private GameObject playerObj;

        private float controlsY;
        private float controlsYDest;
        public float controlsYDest_hidden1;
        public float controlsYDest_hidden2;
        public float controlsYDest_show;
        public float controlsX;

        public KeyCode showButton;

        private bool show;

        void Start()
        {
            camObj = GameObject.FindGameObjectWithTag("MainCamera");
            playerObj = GameObject.FindGameObjectWithTag("Player");

            show = false;
            controlsY = 350;
            controlsYDest = 350;
        }

        void Update()
        {
            bool introScreen = camObj.GetComponent<ScrCamera>().introScreen;

            if (introScreen) {
                controlsYDest = controlsYDest_hidden1;
            }
            else {
                if (Input.GetKeyDown(showButton)) {
                    show = !show;
                }

                if (show) {
                    controlsYDest = controlsYDest_show;
                }
                else {
                    controlsYDest = controlsYDest_hidden2;
                }
            }
            
            controlsY = approach(controlsY, controlsYDest, 12);

            RectTransform controlsTransform = GetComponent<RectTransform>();
            //Vector3 screenDim = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            controlsTransform.anchoredPosition = new Vector3(controlsX, controlsY, 0);
        }

        float approach(float value, float valueDest, float rate) {
            
            if (value < valueDest) {
                value += Mathf.Abs(value - valueDest) / rate;
            }
            else if (value > valueDest) {
                value -= Mathf.Abs(value - valueDest) / rate;
            }
            return value;
        }
    }
}