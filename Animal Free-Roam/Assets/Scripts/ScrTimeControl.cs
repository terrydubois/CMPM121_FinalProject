using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrTimeControl : MonoBehaviour
{
    // following tutorial: https://www.youtube.com/watch?v=MOuS2Wuntl8

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;

    [Range(0, 1)] [SerializeField] private float currentTime = 0;
    private float timeMulti = 1f;
    private float sunIntensityInit;

    void Start()
    {
        sunIntensityInit = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        currentTime += (Time.deltaTime / secondsInFullDay) * timeMulti;

        if (currentTime >= 1) {
            currentTime = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        
        float intenseMulti = 1;
        if (currentTime <= 0.23f || currentTime >= 0.75f) {
            intenseMulti = 0;
        }
        else if (currentTime <= 0.25f) {
            intenseMulti = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
        }
        else if (currentTime >= 0.73f) {
            intenseMulti = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunIntensityInit * intenseMulti;
    }
}
