using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MyList : MonoBehaviour
{
	[SerializeField] TMP_Text numberOfItems;
	[SerializeField] Transform content;

	public List<Item> GetAllItems()
	{
		return content.GetComponentsInChildren<Item>().ToList();
	}

	public void AddItem(Item item)
	{
		if (item.transform.parent == content)
		{
			UpdateItem(item);
		}
		else
		{
            item.transform.SetParent(content);
        }
    }

	public void SwapItems(Item item1, Item item2)
	{
		int item1Index = item1.transform.GetSiblingIndex();
		int item2Index = item2.transform.GetSiblingIndex();

		item1.transform.SetSiblingIndex(item2Index);
		item2.transform.SetSiblingIndex(item1Index);
    }

	void UpdateItem(Item item)
	{
		int index = item.transform.GetSiblingIndex();
		item.transform.SetParent(null);
		item.transform.SetParent(content);
		item.transform.SetSiblingIndex(index);
    }

    void UpdateDisplayedItems()
    {
		numberOfItems.text = content.transform.childCount.ToString();
    }

	// Unity
	private void Start()
	{
		Item.OnParentChanged += _ => UpdateDisplayedItems();

        UpdateDisplayedItems();
    }
}
