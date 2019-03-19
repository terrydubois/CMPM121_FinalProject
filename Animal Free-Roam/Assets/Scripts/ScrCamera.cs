using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Suriyun {
    public class ScrCamera : MonoBehaviour
    {
        public bool introScreen = true;
        
        static int characterAmount = 1;
        public GameObject[] characters = new GameObject[characterAmount];
        private int currentCharacter = 0;
        
        public int fruitPerSound;
        public int score;
        public Text scoreText;

        // Following tutorial: https://www.youtube.com/watch?v=Ta7v27yySKs

        public Transform lookAt;
        public Transform camTransform;

        private Camera cam;

        private float distance = 2.0f;
        private float currentX = 0.0f;
        private float currentY = 0.0f;
        private float sensX = 4.0f;
        private float sensY = 1.0f;

        private const float Y_ANGLE_MIN = 20.0f;
        private const float Y_ANGLE_MAX = 60.0f;
        private const float minDist = 4.0f;
        private const float maxDist = 10.0f;

        private GameObject[] NPCCount;
        private GameObject[] fruitCount;

        public AudioSource[] tracks;
        public AudioSource eatAudio;

        private float logoY;
        private float pressSpaceY;
        public GameObject logoObj;
        public GameObject logoBackObj;
        public GameObject pinwheelObj;
        public GameObject pressSpaceObj;
        public GameObject textObj;

        public GameObject hungerBarBGObj;
        public GameObject hungerBarObj;
        private float barProgress;
        private float hungerBarY;


        private void Start() {
            camTransform = transform;
            cam = Camera.main;

            score = 0;
            setScoreText();

            for (int i = 0; i < tracks.Length; i++) {
                tracks[i].volume = 0;
            }

            logoY = 1300;
            pressSpaceY = -600;

            barProgress = 0;
            hungerBarY = -18;
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

            
            if (introScreen) {
                if (Input.GetKeyUp(KeyCode.Space)) {
                    introScreen = false;
                }

                currentX += 0.25f;
                currentY = 20.0f;
                distance = maxDist;
            }
            else {
                if (Input.GetKeyUp(KeyCode.Escape)) {
                    introScreen = true;
                }

                currentX += Input.GetAxis("Mouse X") * sensX;
                currentY += Input.GetAxis("Mouse Y") * -sensY;

                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

                distance -= Input.GetAxis("Mouse ScrollWheel");
            }

            distance = Mathf.Clamp(distance, minDist, maxDist);

            setScoreText();
            soundControl();
            showMenu();
            controlHungerBar();
        }

        private void LateUpdate() {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rot * dir;
            camTransform.LookAt(lookAt.position);
        }

        void setScoreText() {
            scoreText.text = "Fruit eaten: " + score.ToString();

            RectTransform textTransform = textObj.GetComponent<RectTransform>();
            textTransform.anchoredPosition = new Vector3(0, hungerBarY, 0);
        }

        void soundControl() {

            for (int i = 1; i < tracks.Length + 1; i++) {

                if (score >= i * fruitPerSound) {
                    if (tracks[i - 1].volume < 0.4f) {
                        tracks[i - 1].volume += 0.02f;
                    }
                }
                tracks[i - 1].volume = Mathf.Clamp(tracks[i - 1].volume, 0.0f, 0.4f);
            }
        }

        void controlHungerBar() {

            if (Input.GetKeyDown(KeyCode.L)) {
                score++;
            }

            float barProgressDest = 0;
            float scoreAdjust = Mathf.Clamp(score, 0, tracks.Length * fruitPerSound);
            float trackLenthsFloat = tracks.Length;

            barProgressDest = scoreAdjust / (trackLenthsFloat * fruitPerSound);

            barProgress = approach(barProgress, barProgressDest, 6);

            hungerBarObj.GetComponent<Image>().fillAmount = barProgress;

            float hungerBarYDest = 30;
            if (introScreen) {
                hungerBarYDest = -20;
            }
            hungerBarY = approach(hungerBarY, hungerBarYDest, 10);
            RectTransform hungerBarTransform = hungerBarBGObj.GetComponent<RectTransform>();
            hungerBarTransform.anchoredPosition = new Vector3(0, hungerBarY, 0);
        }

        void showMenu() {

            float logoYDest = 1300;
            float pressSpaceYDest = -600;
            
            if (introScreen) {
                logoYDest = 0;
                pressSpaceYDest = -150;
            }

            logoY = approach(logoY, logoYDest, 20);
            pressSpaceY = approach(pressSpaceY, pressSpaceYDest, 20);

            RectTransform logoTransform = logoObj.GetComponent<RectTransform>();
            logoTransform.anchoredPosition = new Vector3(0, logoY + 20, 0);
            RectTransform logoBackTransform = logoBackObj.GetComponent<RectTransform>();
            logoBackTransform.anchoredPosition = new Vector3(0, logoY, 0);
            RectTransform pinwheelTransform = pinwheelObj.GetComponent<RectTransform>();
            pinwheelTransform.anchoredPosition = new Vector3(0, logoY, 0);

            RectTransform pressSpaceTransform = pressSpaceObj.GetComponent<RectTransform>();
            pressSpaceTransform.anchoredPosition = new Vector3(0, pressSpaceY, 0);
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