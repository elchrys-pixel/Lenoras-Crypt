using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Developer Mode")]
    [SerializeField] private bool enableDevMode;
    [SerializeField] private int sceneID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DevModeOnly()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) // QUICK RESET LEVEL
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}
