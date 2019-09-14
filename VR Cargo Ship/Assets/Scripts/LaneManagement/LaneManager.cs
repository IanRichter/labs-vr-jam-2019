using UnityEngine;

public class LaneManager : MonoBehaviour {

	[Header("Lanes Config")]
	public int numLanes = 10;
	[Tooltip("A value between 0 and 1 representing the size of the ship spawn zone")]
	public float spawnZoneSize = 0f;
	[Tooltip("A value between 0 and 1 representing the size of the finish zone")]
	public float finishZoneSize = 0f;
	
	private SpawnLane[] lanes;


	public int TotalEmptyLanes {
		get {
			return 0; // Stub
		}
	}

	public bool IsLaneEmpty(int lane) {
		return false; // Stub
	}

	public int FirstEmptyLane {
		get {
			return 0; // Stub
		}
	}

	public int NumberOfEntityType(EntitySpawnConfig entityConfig) {
		return 0; // Stub
	}

	public void SpawnEntity(EntitySpawnConfig entityConfig, int lane, EntityMoveDirection direction) {
		// Stub
	}

}
