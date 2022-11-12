using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] new TMP_Text name;
    [SerializeField] TMP_Text id;

    public string Name => name.text;
    public string Id => id.text;

    public static event Action<Item> OnParentChanged;

    MyList parentList;

    public void Init(ItemData itemData)
    {
        name.text = itemData.Name;
        id.text = itemData.Id;
    }

    public ItemData GetData()
    {
        return new ItemData()
        {
            Name = Name,
            Id = Id
        };
    }

    MyList CheckForListSwap()
    {
        foreach (GameObject element in GetUiElementsUnderMouse())
        {
            MyList myList = element.GetComponent<MyList>();

            if (myList != null && myList != parentList)
            {
                parentList = myList;
                parentList.AddItem(this);
                OnParentChanged?.Invoke(this);
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
