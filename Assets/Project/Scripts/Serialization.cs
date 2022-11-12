using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Serialization : MonoBehaviour
{
	[SerializeField] Button save;
	[SerializeField] Button load;

	const string SAVE_DIRECTORY = "Save";

	public void Serialize()
	{
		List<MyList> myLists = FindObjectsOfType<MyList>().ToList();

		foreach (MyList myList in myLists)
		{
            string json = JsonConvert.SerializeObject(myList.GetAllItemsData(), Formatting.Indented);
			Directory.CreateDirectory(SAVE_DIRECTORY);
			File.WriteAllText($@"{SAVE_DIRECTORY}\{myList.Name}.save", json);
        }
	}

	public void Deserialize()
	{
		if (!Directory.Exists(SAVE_DIRECTORY)) { return; }

        List<MyList> myLists = FindObjectsOfType<MyList>().ToList();

        foreach (string fileName in Directory.GetFiles(SAVE_DIRECTORY))
		{
			string listName = Path.GetFileNameWithoutExtension(fileName);
			string json = File.ReadAllText(fileName);
            MyList myList = myLists.First(x => x.Name == listName);
			List<ItemData> itemsData = JsonConvert.DeserializeObject<List<ItemData>>(json);

			if (myList == null) { return; }

			myList.Init(itemsData);
        }
	}

	// Unity
	private void Start()
	{
		save.onClick.AddListener(Serialize);
		load.onClick.AddListener(Deserialize);
	}
}
