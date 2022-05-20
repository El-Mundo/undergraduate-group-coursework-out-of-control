using UnityEngine;

public class ElectricityBehaviour : MonoBehaviour
{
    public int index;
    public bool jumpIn = false, runIn = false;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Moving"))
        {
            DraggableUI button = collision.GetComponent<DraggableUI>();
            if (button == null) return;
            if (button.keyName == "Jump")
            {
                GameManager.instance.SetSceneBool(index, true);
                jumpIn = true;
            }
            else if (button.keyName == "Run")
            {
                GameManager.instance.SetSceneBool(index, true);
                runIn = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Moving"))
        {
            DraggableUI button = collision.GetComponent<DraggableUI>();
            if (button == null) return;
            if (button.keyName == "Jump") 
            {
                jumpIn = false;
            }
            else if (button.keyName == "Run")
            {
                runIn = false;
            }
            if(!jumpIn && !runIn) GameManager.instance.SetSceneBool(index, false);
        }
    }

    private void Update()
    {
        if(transform.localScale.y <= 0.02f)
        {
            jumpIn = runIn = false;
            GameManager.instance.SetSceneBool(index, false);
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }

}
