using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    MyList parentList;

    MyList CheckForListSwap()
    {
        foreach (GameObject element in GetUiElementsUnderMouse())
        {
            MyList myList = element.GetComponent<MyList>();

            if (myList != null && myList != parentList)
            {
                parentList = myList;
                parentList.AddItem(this);
            }
        }

        return null;
    }

    void CheckForItemSpaw()
    {
        foreach (GameObject element in GetUiElementsUnderMouse())
        {
            Item item = element.GetComponent<Item>();
            if (item != null && item != this)
            {
                parentList.SwapItems(this, item);
            }
        }
    }

    List<GameObject> GetUiElementsUnderMouse()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.pointerId = -1;
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Select(x => x.gameObject).ToList();
    }

    // Unity
    private void Start()
    {
        parentList = GetComponentInParent<MyList>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        CheckForListSwap();
        CheckForItemSpaw();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MyList listUnderMouse = CheckForListSwap();
        parentList = listUnderMouse != null ? listUnderMouse : parentList;
        parentList.AddItem(this);
    }
}
