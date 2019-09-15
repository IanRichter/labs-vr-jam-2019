using UnityEngine;
using TMPro;

public class CargoUI : MonoBehaviour {

	public TextMeshPro textDisplay;


	public void SetCargo(int crates) {
		textDisplay.text = crates.ToString();
	}

}
