using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("Config")]
	public int maxHealth = 3;
	public float crateScoreModifier = 10f;

	[Header("Prefabs")]
	public GameObject playerShipPrefab;

	[Header("Player Management")]
	public float finishLineOffset = 0f;
	public Transform playerSpawnPoint;

	[Header("Components")]
	public SpawnHeuristic spawnHeuristic;

	// Game state
	private int health = 0;
	private int score = 0;
	private int level = 1;

	private PlayerShip currentShip;


	private void Start() {
		SpawnShip();
	}

	private void Update() {
		if (!currentShip) {
			return;
		}

		if (currentShip.transform.position.z >= finishLineOffset) {
			ReachFinishLine();
		}
	}

	private void SpawnShip() {
		currentShip = Instantiate(playerShipPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerShip>();
	}

	private void ReachFinishLine() {
		score += Mathf.FloorToInt(currentShip.Crates * crateScoreModifier);
		Destroy(currentShip.gameObject);

		level++;
		spawnHeuristic.CurrentLevel = level;
		SpawnShip();
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
	}

}
