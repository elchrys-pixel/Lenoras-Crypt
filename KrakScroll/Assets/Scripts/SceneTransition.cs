using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Scene_1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Scene_2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            Scene_3();
        }
    }
    private void Scene_1()
    {
        SceneManager.LoadScene("Scene_1");
    }
    private void Scene_2()
    {
        SceneManager.LoadScene("Scene_2");
    }
    private void Scene_3()
    {
        SceneManager.LoadScene("Scene_3");
    }
}
