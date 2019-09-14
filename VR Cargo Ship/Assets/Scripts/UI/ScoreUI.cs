using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour {

	public TextMeshPro textDisplay;


	public void SetScore(int score) {
		textDisplay.text = score.ToString();
	}

}
