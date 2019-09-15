using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

	public TextMeshPro textDisplay;


	public void SetLevel(int level) {
		textDisplay.text = level.ToString();
	}

}
