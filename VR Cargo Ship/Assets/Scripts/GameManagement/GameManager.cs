using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("Config")]
	public int maxHealth = 3;
	public float crateScoreModifier = 10f;

	[Header("Components")]
	public PlayerManager playerManager;
	public SpawnHeuristic spawnHeuristic;
	// public GameObject shipSelectMenu;
	// public GameObject tutorialMenu;

	// Game state
	private int health = 0;
	private int score = 0;
	private int level = 1;

	private PlayerShip currentShip;


	private void Start() {
		playerManager.OnReachFinish += PlayerShipFinishHandler;
	}

	private void PlayerShipFinishHandler() {
		score += Mathf.FloorToInt(currentShip.Crates * crateScoreModifier);
	}

}
