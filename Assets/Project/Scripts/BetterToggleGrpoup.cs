using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class BetterToggleGrpoup : MonoBehaviour
{
	List<BetterToggle> toggles = new List<BetterToggle>();

	public void AddToggle(BetterToggle toggle)
	{
		toggles.Add(toggle);
        toggle.OnStateChanged += _ => ToggleStateChanged(toggle);
    }

	// Events
	void ToggleStateChanged(BetterToggle changedToggle)
	{
        foreach (BetterToggle toggle in toggles)
        {
            if (toggle != changedToggle)
			{
				toggle.SetStateWithoutNotify(BetterToggle.ToggleState.Off);
			}
        }
    }
}
