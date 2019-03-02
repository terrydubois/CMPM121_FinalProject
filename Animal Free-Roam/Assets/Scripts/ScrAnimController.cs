using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrAnimController : MonoBehaviour
{
    // Followed tutorial: https://www.youtube.com/watch?v=N73EWquTGSY
    public Animator anim;
    //public static int fruitNeeded = 1;
    public GameObject[] fruit;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        int fruitEaten = 0;
        for (int i = 0; i < fruit.Length; i++) {
            if (!fruit[i]) {
                fruitEaten++;
            }
        }

        if (fruitEaten == fruit.Length) {
            anim.Play("doorAnimation");
        }
    }
}