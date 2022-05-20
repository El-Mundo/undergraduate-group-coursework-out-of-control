using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecting : MonoBehaviour
{
    public Button[] buttons;

    public void Init()
    {
        int prog = PlayerPrefs.GetInt("Progress", 0);
        int i = 0;
        foreach(Button b in buttons)
        {
            b.interactable = i <= prog;
            i++;
        }
    }

}
