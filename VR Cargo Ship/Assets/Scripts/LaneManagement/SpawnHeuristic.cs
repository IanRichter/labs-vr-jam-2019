using UnityEngine;

public class SpawnHeuristic : MonoBehaviour {

	public LaneManager laneManager;

	[Header("Entity Config")]
	public EntitySpawnConfig[] entityConfigs;

	private int currentLevel;
	public int CurrentLevel {
		set {
			currentLevel = value;
		}
	}

}
