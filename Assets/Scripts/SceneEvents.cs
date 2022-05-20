using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEvents : MonoBehaviour
{
    private static readonly string[] TUTORIAL_STRING = {
        "Dragging the UI buttons in the bottom into the scene can make them materialize in the scene."
        , "You can get the key back by dragging it back to you UI panel.",
        "You can also drag a direction key onto the green field. It will move toward the direction it points to."
           , "But be careful that you cannot retrieve the key back until it gets out of the field!"
    };

    private static readonly string[] TUTORIAL_STRING_1 = {
        "You can drag the UP key onto that green field to move onto the platform above."
        , "You will have to be fast in order not to miss the floatin key through."
    };

    private static readonly string[] L1_STRING = {
        "It looks like you have to sacrifice a key to activate the button above.",
        "I would recommend you keep the UP key, as you have to use it for using the level-clear flag."
    };

    public int code = 0;

    private void TutorialSetHint(int index)
    {
        GameManager.instance.levelHint = index == 0 ? TUTORIAL_STRING : TUTORIAL_STRING_1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (code) {
                case 0:
                case 1:
                    TutorialSetHint(code);
                    break;
                case 2:
                    GameManager.instance.levelHint = L1_STRING;
                    break;
            }
        }
    }

}
