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

    private static readonly string[] L2_STRING = {
        "This place is asking you to sacrifice two keys, wow!",
        "I will recommend here that you sacrifice JUMP and LEFT, as you cannot get through the rabbit without running.",
        "Don't forget that the rabbit is completely harmless when the carrot is still in his hands! You can pass through him without jumping."
    };

    private static readonly string[] L3_STRING = {
        "This scene appears quit messy, huh? So I will put it straightforward.",
        "Put the run key in the red area. After the green field fills this scene, you jump onto your up key in the green field.",
        "On the platform above, turn off the switch and get your up key back, then use it to press the button above.",
        "After these, the door will open. You can get down and enter the door.",
        "You can get your UP key back using the switch beside the flag.",
        "You will be a really hardcore player if you can solve this puzzle!"
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
                case 3:
                    GameManager.instance.levelHint = L2_STRING;
                    break;
                case 4:
                    GameManager.instance.levelHint = L3_STRING;
                    break;
            }
        }
    }

}
