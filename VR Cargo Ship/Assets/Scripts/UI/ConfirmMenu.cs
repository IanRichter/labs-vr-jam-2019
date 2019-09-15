using UnityEngine;

public class ConfirmMenu : MonoBehaviour {

	public ConfirmMenuButton confirmButton;

	public delegate void ConfirmEvent();
	public ConfirmEvent OnConfirm;


	private void Start() {
		confirmButton.OnClick += ButtonClickHandler;
	}

	private void ButtonClickHandler() {
		Debug.Log("MenuConfirmed");
		OnConfirm?.Invoke();
	}

}
