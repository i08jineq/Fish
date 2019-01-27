using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverComponent : MonoBehaviour, IPointerEnterHandler
{
    public EventPublisher onHovered = new EventPublisher();

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        onHovered.Invoke();
    }
}
