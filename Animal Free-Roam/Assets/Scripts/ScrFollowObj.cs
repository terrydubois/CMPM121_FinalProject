using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrFollowObj : MonoBehaviour
{
    public GameObject followObj;

    void Start()
    {
        InvokeRepeating("newPos", 3.0f, 3.0f);
    }

    void Update()
    {
    }

    void newPos() {
        transform.position = followObj.transform.position;   
    }
}
