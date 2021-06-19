using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI losingPointsText;

    [SerializeField] SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        losingPointsText.text = "You've collected: " + Mathf.Round(PlayerPrefs.GetFloat("Points")) + " points";
    }

    public void BackToMainMenu()
    {
        _sceneLoader.LoadScene("MainMenu");
    }
}
