using System;
using UnityEngine;
using UnityEngine.UI;

public class BetterToggle : MonoBehaviour
{
	public enum ToggleState
	{
		Off,
		Up,
		Down
	}

	[SerializeField] Button button;
	[SerializeField] Transform offImage;
	[SerializeField] Transform upImage;
	[SerializeField] Transform downImage;
	[SerializeField] BetterToggleGrpoup betterToggleGroup;

    public ToggleState State { get; private set; }

	public event Action<ToggleState> OnStateChanged;

	public void SetState(ToggleState newState)
	{
		SetStateWithoutNotify(newState);
        OnStateChanged?.Invoke(State);
    }

    public void SetStateWithoutNotify(ToggleState newState)
    {
        DisableAllImages();
        State = newState;

        switch (State)
        {
            case ToggleState.Off:
                offImage.gameObject.SetActive(true);
                break;

            case ToggleState.Up:
                upImage.gameObject.SetActive(true);
                break;

            case ToggleState.Down:
                downImage.gameObject.SetActive(true);
                break;
        }
    }

    void DisableAllImages()
	{
		offImage.gameObject.SetActive(false);
        upImage.gameObject.SetActive(false);
        downImage.gameObject.SetActive(false);
    }

	// Unity
	private void Awake()
	{
		if (betterToggleGroup != null)
		{
            betterToggleGroup.AddToggle(this);
        }
	}

	private void Start()
	{
		button.onClick.AddListener(ButtonClicked);
	}

	// Events
	void ButtonClicked()
	{
		switch (State)
		{
			case ToggleState.Off:
				SetState(ToggleState.Up);
                break;

			case ToggleState.Up:
                SetState(ToggleState.Down);
                break;

			case ToggleState.Down:
                SetState(ToggleState.Up);
                break;
		}
    }
}
