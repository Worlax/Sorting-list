using Unity.VisualScripting;
using UnityEngine;

public class MyList : MonoBehaviour
{
	[SerializeField] Transform content;

	Canvas canvas;

	public void AddItem(Item item)
	{
		item.transform.SetParent(content);
    }

	public void RemoveItem(Item item)
	{
		if (item.transform.parent == content)
		{
			item.transform.SetParent(canvas.transform);
        }
	}

	// Unity
	private void Start()
	{
		canvas = GetComponentInParent<Canvas>();
	}
}
