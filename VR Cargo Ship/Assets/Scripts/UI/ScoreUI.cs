using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour {

	public TextMeshPro textDisplay;


	public void SetScore(int score) {
		Debug.Log("Score updated");
		textDisplay.text = score.ToString();
	}

}
