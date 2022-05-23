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
        "This scene appears quite messy, huh? So I will put it straightforward.",
        "Put the run key in the red area. After the green field fills this scene, you jump onto your up key in the green field.",
        "On the platform above, turn off the switch and get your up key back, then use it to press the button above.",
        "After these, the door will open. You can get down and enter the door.",
        "You can get your UP key back using the switch beside the flag.",
        "You will be a really hardcore player if you can solve this puzzle!"
    };

    private static readonly string[] L4_STRING = {
        "To clear this area, you can put your run key beneath the purple area and push it into the red field with your up key.",
        "You can manipulate your up key's movement with the button beside the machine. Don't forget to stop it timely!",
        "Also, I would suggest that you do not retrieve your up key so quickly. You will see why on the platform above."
    };

    private static readonly string[] L4_STRING_1 = {
        "This area is asking you to perform a run jump to get to the flag. That's why you should consider leave your up key in the area below.",
        "You can start up the machine below again using the green button here. It will help you get your run key back."
    };

    private static readonly string[] L6_STRING = {
        "Use your jump key here!",
        "See? The rabbits on the right to the door will give you no chance if you cannot run!"
    };

    private static readonly string[] L6_STRING_1 = {
        "You can put your run key in the red area to open the door.",
        "But the key itself may block your way? Don't worry, sacrifice your right key to push yourself! It will also push the run key!",
        "You will be OUT OF CONTROL after this puzzle~"
    };

    private static readonly string[] L6_STRING_2 = {
        "You did it! Now you're completely OUT OF CONTROL after these puzzles!",
        "Don't worry! You still have the UP key to clear this level."
    };

    private static readonly string[] L5_STRING = {
        "Be careful! Don't fall into the large gap below!",
        "You can repeatedly use two keys as step stones to traverse it.",
        "For example, you first jump onto the left key, and then the up key.",
        "After landing on the up key, you use the left key again as the next step stone."
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
                case 5:
                    GameManager.instance.levelHint = L4_STRING;
                    break;
                case 6:
                    GameManager.instance.levelHint = L4_STRING_1;
                    break;
                case 7:
                    GameManager.instance.levelHint = L6_STRING;
                    break;
                case 8:
                    GameManager.instance.levelHint = L6_STRING_1;
                    break;
                case 9:
                    GameManager.instance.levelHint = L6_STRING_2;
                    break;
                case 10:
                    GameManager.instance.levelHint = L5_STRING;
                    break;
            }
        }
    }

}
