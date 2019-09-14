using UnityEngine;

public class LaneManager : MonoBehaviour {

	[Header("Lanes Config")]
	public int numLanes = 10;
	public Transform startPoint;
	public Transform endPoint;
	public GameObject laneContainer;
	public GameObject lanePrefab;

	private SpawnLane[] lanes;


	private void Start() {
		GenerateLanes(startPoint.position, endPoint.position);
	}

	private void GenerateLanes(Vector3 startPoint, Vector3 endPoint) {
		lanes = new SpawnLane[numLanes];
		for (int i = 0; i < numLanes; i++) {
			Vector3 position = Vector3.Lerp(startPoint, endPoint, i / (float)(numLanes - 1));
			//SpawnLane lane = Instantiate(lanePrefab, position, Quaternion.identity).GetComponent<SpawnLane>();
			SpawnLane lane = Instantiate(lanePrefab, position, Quaternion.identity).GetComponent<SpawnLane>();
			lanes[i] = lane;
		}
	}

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

	public void SpawnEntity(EntitySpawnConfig entityConfig, int laneNum, EntityMoveDirection direction) {
		lanes[laneNum].SpawnEntity(entityConfig, direction);
	}

}
