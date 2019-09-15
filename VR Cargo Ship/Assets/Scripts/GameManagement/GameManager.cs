using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("GameState Config")]
	public int maxHealth = 3;
	public float crateScoreModifier = 10f;

	[Header("Player Management")]
	public GameObject playerShipPrefab;
	public float finishLineOffset = 0f;
	public Transform playerSpawnPoint;
	public float playerMapEdge = 0.8f;

	[Header("Menus")]
	public ConfirmMenu startScreen;
	public ConfirmMenu gameOverScreen;

	[Header("Gameplay UI")]
	public ScoreUI scoreDisplay;
	public HealthUI healthDisplay;
	public LevelUI levelDisplay;

	[Header("Components")]
	public SpawnHeuristic spawnHeuristic;
	public InputManager inputManager;

	private enum GameState {
		Start,
		Tutorial,
		GamePlay,
		GameOver
	}

	private GameState activeGameState = GameState.Start;

	// Game state
	private int health = 0;
	private int score = 0;
	private int level = 1;
	private PlayerShip playerShip;

	private int Score {
		get {
			return score;
		}
		set {
			score = value;
			//scoreDisplay.SetScore(score);
		}
	}

	private int Health {
		get {
			return health;
		}
		set {
			health = value;
			//healthDisplay.SetHealth(health);
		}
	}

	private int Level {
		get {
			return level;
		}
		set {
			level = value;
			//levelDisplay.SetLevel(level);
		}
	}

	private void ResetGameState() {
		Health = maxHealth;
		Score = 0;
		Level = 1;
	}


	private void Start() {

		//startScreen.OnConfirm += StartScreenConfirmHandler;
		//gameOverScreen.OnConfirm += GameOverScreenConfirmHandler;

		ResetGameState();
		StartGame();
	}

	private void Update() {
		switch (activeGameState) {
			case GameState.Start:
				// Stub
				break;

			case GameState.Tutorial:
				activeGameState = GameState.GamePlay;
				break;

			case GameState.GamePlay:
				if (!playerShip) {
					SpawnShip();
				}

				if (IsShipOverFinish) {
					PlayerReachFinishLine();
				}
				break;

			case GameState.GameOver:
				// Stub
				break;

			default:
				Debug.Log("Invalid game state");
				return;
		}
	}

	private void StartScreenConfirmHandler() {
		StartGame();
	}

	private void GameOverScreenConfirmHandler() {
		StartGame();
	}

	private void StartGame() {
		ResetGameState();

		activeGameState = GameState.GamePlay;
	}

	private void GameOver() {
		activeGameState = GameState.GameOver;
	}

	private bool IsShipOverFinish {
		get {
			if (!playerShip) {
				return false;
			}

			return playerShip.transform.position.z >= finishLineOffset;
		}
	}

	private void SpawnShip() {
		playerShip = Instantiate(playerShipPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerShip>();
		playerShip.inputManager = inputManager;
		playerShip.mapEdge = playerMapEdge;
		playerShip.OnPlayerDeath += PlayerDeathHandler;
	}

	private void PlayerReachFinishLine() {
		Score += Mathf.FloorToInt(playerShip.Crates * crateScoreModifier);
		Level++;
		spawnHeuristic.CurrentLevel = Level;
		
		Destroy(playerShip.gameObject);
	}

	private void PlayerDeathHandler() {
		playerShip.OnPlayerDeath -= PlayerDeathHandler;

		Health--;
		if (Health <= 0) {
			GameOver();
		}
	}

	private void OnDrawGizmos() {
		// Spawn point
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(playerSpawnPoint.position, 0.1f);
		
		// Finish line
		Gizmos.color = Color.red;
		Vector3 finishLine = new Vector3(
			playerSpawnPoint.position.x,
			playerSpawnPoint.position.y,
			finishLineOffset
		);
		float lineLength = 1f;
		Gizmos.DrawLine(
			finishLine - new Vector3(lineLength, 0, 0),
			finishLine - new Vector3(-lineLength, 0, 0)
		);

		// Player Map Edge
		Gizmos.color = Color.blue;
		lineLength = 2f;
		Gizmos.DrawLine(
			playerSpawnPoint.position + new Vector3(-playerMapEdge, 0, 0),
			playerSpawnPoint.position + new Vector3(-playerMapEdge, 0, lineLength)
		);
		Gizmos.DrawLine(
			playerSpawnPoint.position + new Vector3(playerMapEdge, 0, 0),
			playerSpawnPoint.position + new Vector3(playerMapEdge, 0, lineLength)
		);
	}

}
