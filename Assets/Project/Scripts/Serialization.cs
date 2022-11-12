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

	// Creating save file for each "itemList" we find in the scene
	// and saving information about items inside
	public void Serialize()
	{
		List<ItemsList> myLists = FindObjectsOfType<ItemsList>().ToList();

		foreach (ItemsList itemsList in myLists)
		{
			string json = JsonConvert.SerializeObject(itemsList.GetAllItemsData(), Formatting.Indented);
			Directory.CreateDirectory(SAVE_DIRECTORY);
			File.WriteAllText($@"{SAVE_DIRECTORY}\{itemsList.Name}.save", json);
		}
	}


    // Loading files that we saved in "Serialize" function
	// and initializing itemLists we found on the scene with the content
	// of the name-matching files
    public void Deserialize()
	{
		if (!Directory.Exists(SAVE_DIRECTORY)) { return; }

        List<ItemsList> myLists = FindObjectsOfType<ItemsList>().ToList();

        foreach (string fileName in Directory.GetFiles(SAVE_DIRECTORY))
		{
            string listName = Path.GetFileNameWithoutExtension(fileName);
            ItemsList itemsList = myLists.First(x => x.Name == listName);

            if (itemsList == null) { return; }

			string json = File.ReadAllText(fileName);
			List<ItemData> itemsData = JsonConvert.DeserializeObject<List<ItemData>>(json);

			itemsList.Init(itemsData);
        }
	}

	// Unity
	private void Start()
	{
		save.onClick.AddListener(Serialize);
		load.onClick.AddListener(Deserialize);
	}
}
