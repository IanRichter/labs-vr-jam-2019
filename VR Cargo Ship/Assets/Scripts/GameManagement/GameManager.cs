using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("Config")]
	public int maxHealth = 3;
	public float crateScoreModifier = 10f;

	// Game state
	private int health = 0;
	private int score = 0;


	public void LoseLife() {
		// Stub
	}

}
