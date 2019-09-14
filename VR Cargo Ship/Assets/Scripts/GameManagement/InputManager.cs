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
			return Input.GetKey(KeyCode.L);
		}
	}

}
