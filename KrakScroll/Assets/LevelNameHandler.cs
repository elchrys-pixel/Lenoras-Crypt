using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNameHandler : MonoBehaviour
{
    public int levelNumber;
    public string levelName13Chars;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelNameText;

    private void Start()
    {
        levelText.text = "Level " + levelNumber;
        levelNameText.text = levelName13Chars;
    }
}
