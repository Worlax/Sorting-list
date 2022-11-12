using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// Item that u can drag around and swap it with other items
// or drag it into in list
public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] new TMP_Text name;
    [SerializeField] TMP_Text id;

    public string Name => name.text;
    public string Id => id.text;

    public static event Action<Item> OnParentChanged;

    ItemsList parentList;

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

    void SwapListIfOver()
    {
        foreach (GameObject element in GetUiElementsUnderMouse())
        {
            ItemsList listOverMouse = element.GetComponent<ItemsList>();

            if (listOverMouse != null && listOverMouse != parentList)
            {
                parentList = listOverMouse;
                parentList.AddItem(this);
                OnParentChanged?.Invoke(this);
            }
        }
    }

    void SpawItemsIfOver()
    {
        foreach (GameObject element in GetUiElementsUnderMouse())
        {
            Item itemOverMouse = element.GetComponent<Item>();
            if (itemOverMouse != null && itemOverMouse != this)
            {
                parentList.SwapItems(this, itemOverMouse);
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
        parentList = GetComponentInParent<ItemsList>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        SwapListIfOver();
        SpawItemsIfOver();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        parentList.UpdateItemPosition(this);
    }
}
