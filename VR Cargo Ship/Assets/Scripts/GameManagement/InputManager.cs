using UnityEngine;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Vive;
using System;
using Valve.VR;

public class InputManager : MonoBehaviour {

	public Hand leftHand;
	public Hand rightHand;

	private bool leftTrigger = false;
	private bool rightTrigger = false;
	private bool gripActive = false;


	private void Start() {
		leftHand.grabPinchAction.AddOnChangeListener(LeftTriggerHandler, SteamVR_Input_Sources.LeftHand);
		rightHand.grabPinchAction.AddOnChangeListener(RightTriggerHandler, SteamVR_Input_Sources.RightHand);
		rightHand.grabGripAction.AddOnChangeListener(GripHandler, SteamVR_Input_Sources.Any);
	}

	public bool IsLeft {
		get {
			//return Input.GetKey(KeyCode.J) || ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger);
			return Input.GetKey(KeyCode.J) || leftTrigger;
		}
	}

	private void LeftTriggerHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) {
		leftTrigger = newState;
	}

	public bool IsRight {
		get {
			//return Input.GetKey(KeyCode.K) || ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger);
			return Input.GetKey(KeyCode.K) || rightTrigger;
		}
	}

	private void RightTriggerHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) {
		rightTrigger = newState;
	}

	public bool IsBoosting {
		get {
			//if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Grip)) {
			//	return true;
			//}

			//if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Grip)) {
			//	return true;
			//}

			return Input.GetKey(KeyCode.L) || gripActive;
		}
	}

	private void GripHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) {
		gripActive = newState;
	}

}
