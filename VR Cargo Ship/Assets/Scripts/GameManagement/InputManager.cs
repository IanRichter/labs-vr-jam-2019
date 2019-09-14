using UnityEngine;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Vive;

public class InputManager : MonoBehaviour {

	public Hand leftHand;
	public Hand rightHand;

	public bool IsLeft {
		get {
			return Input.GetKey(KeyCode.J) || ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger);
		}
	}

	public bool IsRight {
		get {
			return Input.GetKey(KeyCode.K) || ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger);
		}
	}

	public bool IsBoosting {
		get {
			if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Grip)) {
				return true;
			}

			if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Grip)) {
				return true;
			}

			return Input.GetKey(KeyCode.L);
		}
	}

}
