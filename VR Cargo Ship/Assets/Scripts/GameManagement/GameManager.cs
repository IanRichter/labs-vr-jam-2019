using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("Config")]
	public int maxHealth = 3;
	public float crateScoreModifier = 10f;

	[Header("Components")]
	public PlayerManager playerManager;
	public LaneManager laneManager;
	// public GameObject shipSelectMenu;
	// public GameObject tutorialMenu;

	// Game state
	private int health = 0;
	private int score = 0;
	private int level = 1;

	private PlayerShip currentShip;
	

}
