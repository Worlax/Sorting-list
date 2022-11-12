using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Sorting items in list on toggle state change
[RequireComponent(typeof(ItemsList))]
public class ItemsListSort : MonoBehaviour
{
	[SerializeField] TripleToggle sortByNameToggle;
	[SerializeField] TripleToggle sortByIdToggle;

	ItemsList itemsList;

	// Unity
	private void Start()
	{
        itemsList = GetComponent<ItemsList>();

        sortByNameToggle.OnStateChanged += SortByNameStateChanged;
        sortByIdToggle.OnStateChanged += SortByIdStateChanged;
    }

	// Events
	void SortByNameStateChanged(TripleToggle.ToggleState newState)
	{
        OrderItemsBy(x => x.Name, newState == TripleToggle.ToggleState.Up);
    }

	void SortByIdStateChanged(TripleToggle.ToggleState newState)
	{
        OrderItemsBy(x => x.Id, newState == TripleToggle.ToggleState.Up);
    }

	void OrderItemsBy<T>(Func<Item, T> keySelector, bool Descending)
	{
        List<Item> items = itemsList.GetAllItems();

        if (Descending)
        {
            items = items.OrderByDescending(keySelector).ToList();
        }
        else
        {
            items = items.OrderBy(keySelector).ToList();
        }

        for (int i = 0; i < items.Count; ++i)
        {
            items[i].transform.SetSiblingIndex(i);
        }
    }
}
