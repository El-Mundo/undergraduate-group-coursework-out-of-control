using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElfReminderBehaviour : MonoBehaviour
{
    public static ElfReminderBehaviour instance;

    public float easing = 0.05f;
    public Transform target;
    public Texture2D hintCursor;

    public bool isFollowing = true;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFollowing) { 
            Vector3 thisPos = transform.position;
            Vector3 newPos = thisPos;
            newPos.x = thisPos.x + (target.position.x - thisPos.x) * easing * 5;
            newPos.y = thisPos.y + (target.position.y - thisPos.y) * easing * 5;
            transform.position = newPos;
        }
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(hintCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        string[] s = GameManager.instance.levelHint;
        if (s == null) return;
        if (s.Length <= 0) return;

        string[] copy = new string[s.Length];
        int i = 0;
        
        foreach(string ss in s) {
            copy[i] = (i == 0 ? "/s" : "") + "/nLittle Sprite/m" + ss + (i == s.Length - 1 ? "/e" : "");
            i++;
        }
        DialogueManager.instance.StartDialog(copy);
    }

}
