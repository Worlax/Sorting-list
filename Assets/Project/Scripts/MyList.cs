using UnityEngine;

public class MyList : MonoBehaviour
{
	[SerializeField] Transform content;

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
}
