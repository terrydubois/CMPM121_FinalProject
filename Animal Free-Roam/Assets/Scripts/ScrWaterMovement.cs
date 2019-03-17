using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrWaterMovement : MonoBehaviour
{
    public GameObject follow;
    private Transform followTransform;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        followTransform = follow.GetComponent<Transform>();
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position = new Vector3(followTransform.position.x,
                                    myTransform.position.y,
                                    followTransform.position.z);
    }
}
