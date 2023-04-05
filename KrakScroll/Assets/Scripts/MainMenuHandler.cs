using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public RectTransform menuSlider;
    public float mainMenuX;
    public float levelSelectionX;

    public float transitionSpeed;

    private bool displayLevelSelect;

    private void Start()
    {
        menuSlider.anchoredPosition = new Vector2(mainMenuX, menuSlider.anchoredPosition.y);
    }

    private void Update()
    {
        if (displayLevelSelect)
        {
            if (menuSlider.anchoredPosition.x > levelSelectionX) menuSlider.anchoredPosition += new Vector2(-transitionSpeed, 0);
        }
        else if (menuSlider.anchoredPosition.x < mainMenuX) menuSlider.anchoredPosition += new Vector2(transitionSpeed, 0);
    }

    public void StartGame()
    {
        displayLevelSelect = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LaunchLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LaunchLevelTwo()
    {
        if (PlayerPrefs.GetInt("LevelTwoUnlocked") == 1) SceneManager.LoadScene(2);
    }

    public void LaunchLevelThree()
    {
        if (PlayerPrefs.GetInt("LevelThreeUnlocked") == 1) SceneManager.LoadScene(3);
    }

    public void ReturnToMainMenuSelection()
    {
        displayLevelSelect = false;
    }
}
