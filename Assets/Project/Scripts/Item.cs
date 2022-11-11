using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    MyList myList;

    MyList GetListUnderMouse()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.pointerId = -1;
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            MyList myList = result.gameObject.GetComponent<MyList>();
            if (myList != null)
            {
                return myList;
            }
        }

        return null;
    }

    // Unity
    private void Start()
    {
        myList = GetComponentInParent<MyList>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myList.RemoveItem(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MyList listUnderMouse = GetListUnderMouse();
        myList = listUnderMouse != null ? listUnderMouse : myList;
        myList.AddItem(this);
    }
}
