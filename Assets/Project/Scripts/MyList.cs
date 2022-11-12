using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class MyList : MonoBehaviour
{
	[SerializeField] new TMP_Text name;
	[SerializeField] TMP_Text numberOfItems;
	[SerializeField] Item itemPrefab;
	[SerializeField] Transform content;

	public string Name => name.text;

    public List<Item> GetAllItems()
	{
		return content.GetComponentsInChildren<Item>().ToList();
	}

	public List<ItemData> GetAllItemsData()
	{
		return GetAllItems().Select(x => x.GetData()).ToList();
	}

	public void Init(List<ItemData> itemsData)
	{
		DeleteAllItems();

		foreach(ItemData itemData in itemsData)
		{
			Item item = Instantiate(itemPrefab, content);
			item.Init(itemData);
        }

		numberOfItems.text = itemsData.Count.ToString();
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

	void DeleteAllItems()
	{
		foreach (Item item in content.GetComponentsInChildren<Item>())
		{
			Destroy(item.gameObject);
		}
	}

	void UpdateItem(Item item)
	{
		int index = item.transform.GetSiblingIndex();
		item.transform.SetParent(null);
		item.transform.SetParent(content);
		item.transform.SetSiblingIndex(index);
    }

    void UpdateItemsCount()
    {
		numberOfItems.text = content.transform.childCount.ToString();
    }

	// Unity
	private void Start()
	{
		Item.OnParentChanged += _ => UpdateItemsCount();

        UpdateItemsCount();
    }
}
