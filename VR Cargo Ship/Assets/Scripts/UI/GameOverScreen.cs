using UnityEngine;
using TMPro;

public class GameOverScreen : ConfirmMenu {

	public TextMeshPro scoreDisplay;


	public void SetScore(int score) {
		scoreDisplay.text = score.ToString();
	}

}
