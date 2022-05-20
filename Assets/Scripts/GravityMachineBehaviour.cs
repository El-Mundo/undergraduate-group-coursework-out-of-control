using UnityEngine;

public class GravityMachineBehaviour : MonoBehaviour
{
    public bool isOn = false;
    [Tooltip("Set this as -1 so that it will always be on," +
        " otherwise it is powered by a Scene Bool in GameManager.")]
    public int index = -1;
    public GameObject gravity;
    public Animator gravityAnimator;

    private void Start()
    {
        if (index >= 0)
        { 
            isOn = GameManager.instance.GetSceneBool(index);
            gravityAnimator.SetBool("on", isOn);
        }
        else
            gravityAnimator.SetBool("on", true);
    }

    private void Update()
    {
        if (index >= 0)
        {
            bool tar = GameManager.instance.GetSceneBool(index);
            if (tar != isOn)
            {
                isOn = tar;
                gravityAnimator.SetBool("on", isOn);
            }
        }
    }

}
