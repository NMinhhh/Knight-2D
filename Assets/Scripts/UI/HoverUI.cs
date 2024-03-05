using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        InputManager.Instance.MouseClick();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputManager.Instance.MouseShoting();
    }
}
