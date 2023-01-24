using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IBeginDragHandler, IDragHandler
{ 

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("asd");
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("as");
    }
}
