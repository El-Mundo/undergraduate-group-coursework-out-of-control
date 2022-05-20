using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static int loadedLevelIndex = 0;

    public void Level()
    {
        if (loadedLevelIndex == 0)
            SceneManager.LoadScene("Scenes/Tutorial");
        else
            SceneManager.LoadScene("Scenes/" + loadedLevelIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void Empty()
    {
        SceneManager.LoadScene("Scenes/Empty");
    }

}
