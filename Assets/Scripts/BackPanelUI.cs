using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackPanelUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool cursorInBackPanel = false;
    public static BackPanelUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorInBackPanel = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorInBackPanel = false;
    }

}
