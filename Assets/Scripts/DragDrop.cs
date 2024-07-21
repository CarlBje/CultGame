using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    
    //Get transform of the object
    private Transform itemTransform;
    private Vector3 offset;

    void Awake()
    {
        itemTransform = GetComponent<Transform>();
    }
    
    void OnMouseDown()
    {
        offset = itemTransform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        itemTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }



}
