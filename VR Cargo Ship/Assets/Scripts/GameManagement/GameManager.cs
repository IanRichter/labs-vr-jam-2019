using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("GameState Config")]
	public int maxHealth = 3;
	public int maxCrates = 10;
	public float crateScoreModifier = 10f;

	[Header("Player Management")]
	public GameObject playerShipPrefab;
	public float finishLineOffset = 0f;
	public Transform playerSpawnPoint;
	public float playerMapEdge = 0.8f;

	[Header("Menus")]
	public ConfirmMenu startScreen;
	public ConfirmMenu respawnScreen;
	public GameOverScreen gameOverScreen;

	[Header("Gameplay UI")]
	public ScoreUI scoreDisplay;
	public LevelUI levelDisplay;
	public HealthUI healthDisplay;
	public CargoUI crateDisplay;

	[Header("Components")]
	public SpawnHeuristic spawnHeuristic;
	public InputManager inputManager;

	private enum GameState {
		Start,
		Tutorial,
		GamePlay,
		Respawn,
		GameOver
	}

	private GameState activeGameState = GameState.Start;

	// Game state
	private int health = 0;
	private int crates = 0;
	private int score = 0;
	private int level = 1;
	private PlayerShip playerShip;

	private int Score {
		get {
			return score;
		}
		set {
			score = value;
			scoreDisplay.SetScore(score);
		}
	}

	private int Health {
		get {
			return health;
		}
		set {
			health = value;
			healthDisplay.SetHealth(health);
		}
	}

	private int Level {
		get {
			return level;
		}
		set {
			level = value;
			levelDisplay.SetLevel(level);
		}
	}

	private int Crates {
		get {
			return crates;
		}
		set {
			crates = value;
			crateDisplay.SetCargo(crates);
		}
	}

	private void ResetGameState() {
		Health = maxHealth;
		//Crates = maxCrates;
		Score = 0;
		Level = 1;
	}


	private void Start() {
		// Menu Handlers
		startScreen.OnConfirm += StartMenuConfirmHandler;
		respawnScreen.OnConfirm += RespawnMenuConfirmHandler;
		gameOverScreen.OnConfirm += GameOverMenuConfirmHandler;

		// Start
		ShowStartMenu();
	}

	private void Update() {
		switch (activeGameState) {

			case GameState.Start:
				if (Input.GetKeyDown(KeyCode.L)) {
					ShowTutorialMenu();
				}
				break;

			case GameState.Tutorial:
				if (Input.GetKeyDown(KeyCode.L)) {
					StartGame();
				}
				break;

			case GameState.GamePlay:
				if (IsShipOverFinish) {
					PlayerReachFinishLine();
				}
				break;

			case GameState.Respawn:
				if (Input.GetKeyDown(KeyCode.L)) {
					StartGame();
				}
				break;

			case GameState.GameOver:
				if (Input.GetKeyDown(KeyCode.L)) {
					ShowStartMenu();
				}
				break;

			default:
				return;
		}
	}

	// ========================================================================

	private void StartMenuConfirmHandler() {
		if (activeGameState != GameState.Start) {
			return;
		}

		ShowTutorialMenu();
	}

	private void TutorialMenuConfirmHandler() {
		if (activeGameState != GameState.Tutorial) {
			return;
		}

		StartGame();
	}

	private void RespawnMenuConfirmHandler() {
		if (activeGameState != GameState.Respawn) {
			return;
		}

		StartGame();
	}

	private void GameOverMenuConfirmHandler() {
		if (activeGameState != GameState.GameOver) {
			return;
		}

		ShowStartMenu();
	}

	// ========================================================================

	private void ShowMenu(ConfirmMenu menu) {
		startScreen.gameObject.SetActive(menu == startScreen);
		respawnScreen.gameObject.SetActive(menu == respawnScreen);
		gameOverScreen.gameObject.SetActive(menu == gameOverScreen);
	}

	// ========================================================================

	private void ShowStartMenu() {
		Debug.Log("ShowStartMenu");
		activeGameState = GameState.Start;
		ResetGameState();
		ShowMenu(startScreen);
	}

	// TODO: Implement this
	private void ShowTutorialMenu() {
		Debug.Log("ShowTutorialMenu");
		activeGameState = GameState.Tutorial;

		StartGame(); // Temp
	}

	private void StartGame() {
		Debug.Log("StartGame");
		activeGameState = GameState.GamePlay;
		ShowMenu(null);
		SpawnShip();
	}

	private void ShowRespawnMenu() {
		Debug.Log("ShowRespawnMenu");
		activeGameState = GameState.Respawn;
		ShowMenu(respawnScreen);
	}

	private void ShowGameOverMenu() {
		Debug.Log("ShowGameOverMenu");
		activeGameState = GameState.GameOver;
		ShowMenu(gameOverScreen);
	}

	// ========================================================================

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

		playerShip.OnPlayerDamaged += PlayerDamageHandler;
	}

	private void PlayerReachFinishLine() {
		//Score += (int)(Crates * crateScoreModifier);
		Score += (int)(crateScoreModifier * (float)Level);
		Level += 1;
		spawnHeuristic.CurrentLevel = Level;
		
		Destroy(playerShip.gameObject);
		ShowRespawnMenu();
	}

	private void PlayerDamageHandler(int amount) {
		Debug.Log("Player Damaged");
		KillPlayer();
	}

	private void KillPlayer() {
		playerShip.OnPlayerDamaged += PlayerDamageHandler;
		Destroy(playerShip);

		Health -= 1;
		if (Health <= 0) {
			ShowGameOverMenu();
		}
		else {
			ShowRespawnMenu();
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
