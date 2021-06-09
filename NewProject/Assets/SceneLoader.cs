using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string _goToScene)
    {
        SceneManager.LoadScene(_goToScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
