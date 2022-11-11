using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;

public class ListSort : MonoBehaviour
{
	[SerializeField] BetterToggle sortByNameToggle;
	[SerializeField] BetterToggle sortByIdToggle;
	[SerializeField] MyList myList;

	// Unity
	private void Start()
	{
		sortByNameToggle.OnStateChanged += SortByNameStateChanged;
        sortByIdToggle.OnStateChanged += SortByIdStateChanged;
    }

	// Events
	void SortByNameStateChanged(BetterToggle.ToggleState newState)
	{
        OrderItemsBy(x => x.Name, newState == BetterToggle.ToggleState.Up);
    }

	void SortByIdStateChanged(BetterToggle.ToggleState newState)
	{
        OrderItemsBy(x => x.Id, newState == BetterToggle.ToggleState.Up);
    }

	void OrderItemsBy<T>(Func<Item, T> keySelector, bool Descending)
	{
        List<Item> items = myList.GetAllItems();

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
