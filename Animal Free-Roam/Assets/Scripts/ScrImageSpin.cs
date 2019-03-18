using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrImageSpin : MonoBehaviour
{
    public GameObject camObj;
    RectTransform myTransform;
    float currentZ = 0;
    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = camObj.GetComponent<Transform>().localRotation;
        myTransform.localRotation = new Quaternion(0, 0, rot.y, 1);
    }
}
