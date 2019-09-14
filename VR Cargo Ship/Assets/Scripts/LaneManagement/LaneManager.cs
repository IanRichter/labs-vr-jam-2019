using UnityEngine;

public class LaneManager : MonoBehaviour {

	[Header("Lanes Config")]
	public int numLanes = 10;
	[Tooltip("A value between 0 and 1 representing the size of the ship spawn zone")]
	public float spawnZoneSize = 0f;
	[Tooltip("A value between 0 and 1 representing the size of the finish zone")]
	public float finishZoneSize = 0f;

	[Header("Lanes Visuals")]
	public Color laneColorA;
	public Color laneColorB;

	private SpawnLane[] lanes;
	private int currentLevel = 0;


	public void StartLevel(int level) {

	}

	public void StopLevel() {

	}

	public void SpawnObjectInLane(GameObject gameObject, int lane, EntityMoveDirection direction) {

	}

}
