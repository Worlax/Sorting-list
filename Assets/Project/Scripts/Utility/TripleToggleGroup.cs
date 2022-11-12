using System.Collections.Generic;
using UnityEngine;

public class TripleToggleGroup : MonoBehaviour
{
	List<TripleToggle> toggles = new List<TripleToggle>();

	public void AddToggle(TripleToggle toggle)
	{
		toggles.Add(toggle);
        toggle.OnStateChanged += _ => ToggleStateChanged(toggle);
    }

	// Events
	void ToggleStateChanged(TripleToggle changedToggle)
	{
        foreach (TripleToggle toggle in toggles)
        {
            if (toggle != changedToggle)
			{
				toggle.SetStateWithoutNotify(TripleToggle.ToggleState.Off);
			}
        }
    }
}
