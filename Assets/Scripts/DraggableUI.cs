using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private static readonly float VELOCITY = 0.8F;

    private RectTransform thisRect;
    private Collider2D thisCollider;
    private Rigidbody2D thisBody;
    public bool instantiated = false;
    private bool collided = false, inGravity = false;
    private Vector2 startPos, instantiatedPos;
    private Transform startParent;
    [SerializeField]
    private Image img;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text text;

    public bool moving = false;

    public string keyName;

    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
        startPos = thisRect.anchoredPosition;
        thisCollider = GetComponent<Collider2D>();
        thisCollider.enabled = false;
        thisBody = GetComponent<Rigidbody2D>();
        thisBody.simulated = false;
        gameObject.layer = 8;
        startParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (moving) return;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        transform.position = rayPoint;
        img.raycastTarget = false;
        if(text != null)
            text.raycastTarget = false;
        if (icon != null)
            icon.raycastTarget = false;
        thisCollider.enabled = false;
        gameObject.layer = 8;
        thisBody.velocity = Vector2.zero;
        thisBody.simulated = true;
        tag = "Wood";

        instantiated = false;
        PlayerBehaviour.instance.controls.SetKey(keyName, false);
        if (!CanInstantiate())
        {
            img.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            img.color = new Color(1, 1, 1, 1);
        }
    }

    private bool CanInstantiate()
    {
        return !BackPanelUI.instance.cursorInBackPanel && !collided;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (moving) return;

        if (CanInstantiate())
        {
            Instantiate();
        }
        else
        {
            Recycle();
        }
        img.raycastTarget = true;
        if(text != null)
            text.raycastTarget = true;
        if (icon != null)
            icon.raycastTarget = true;
        img.color = new Color(1, 1, 1, 1);
    }

    public void Instantiate()
    {
        thisCollider.enabled = true;
        thisBody.simulated = true;
        gameObject.layer = 0;
        instantiatedPos = transform.position;
        transform.SetParent(GameManager.instance.worldCanvas.transform);
        instantiated = true;
    }

    public void Recycle()
    {
        thisBody.simulated = false;
        PlayerBehaviour.instance.controls.SetKey(keyName, true);
        transform.SetParent(startParent);
        thisRect.anchoredPosition = startPos;
        instantiated = false;
    }

    private void Update()
    {
        moving = false;

        if (instantiated)
        {
            if (inGravity && thisBody.simulated)
            {
                
                if(keyName == "Right") {
                    thisBody.velocity = new Vector2(VELOCITY, 0);
                    moving = true;
                }
                else if(keyName == "Left")
                {
                    thisBody.velocity = new Vector2(-VELOCITY, 0);
                    moving = true;
                }
                else if(keyName == "Up")
                {
                    thisBody.velocity = new Vector2(0, VELOCITY);
                    moving = true;
                }
            }
            else
            {
                thisBody.velocity = new Vector2(0, 0);
            }

            if (moving)
            {
                img.color = Color.green;
                tag = "Moving";
            }
            else
            {
                img.color = Color.white;
                tag = "Wood";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Untagged") || collision.CompareTag("Wood") || collision.CompareTag("Grass"))
        {
            collided = true;
        }
        else if (collision.CompareTag("Gravity"))
        {
            inGravity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Untagged") || collision.CompareTag("Wood") || collision.CompareTag("Grass"))
        {
            collided = false;
        }
        else if (collision.CompareTag("Gravity"))
        {
            inGravity = false;
        }
    }

}
