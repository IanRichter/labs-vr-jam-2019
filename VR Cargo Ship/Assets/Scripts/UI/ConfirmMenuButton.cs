﻿using UnityEngine;

public class ConfirmMenuButton : MonoBehaviour {

	public delegate void ConfirmAction();
	public ConfirmAction OnClick;


	private void OnTriggerEnter(Collider other) {
		if (!other.gameObject.GetComponent<ControllerClickBall>()) {
			return;
		}

		OnClick?.Invoke();
	}

}
