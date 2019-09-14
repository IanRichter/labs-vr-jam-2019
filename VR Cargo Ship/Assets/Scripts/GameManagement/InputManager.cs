using UnityEngine;

public class InputManager : MonoBehaviour {

	public bool IsLeft {
		get {
			return Input.GetKey(KeyCode.J);
		}
	}

	public bool IsRight {
		get {
			return Input.GetKey(KeyCode.K);
		}
	}

	public bool IsBoosting {
		get {
			return Input.GetKey(KeyCode.L);
		}
	}

}
