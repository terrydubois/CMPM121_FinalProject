using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrChangeScene : MonoBehaviour
{
    public void nextScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            nextScene(0);
        }
    }
}