using UnityEngine;

public class LaneManager : MonoBehaviour {

	[Header("Lanes Config")]
	public int numLanes = 10;
	public float laneOffset = 1.0f;
	public Transform startPoint;
	public Transform endPoint;
	public GameObject laneContainer;
	public GameObject lanePrefab;

	private SpawnLane[] lanes;

	private void Start() {
		lanes = new SpawnLane[numLanes];
		GenerateLanes(startPoint.position, endPoint.position);
	}

	private void GenerateLanes(Vector3 startPoint, Vector3 endPoint) {
		
		for (int i = 0; i < numLanes; i++) {
			Vector3 position = Vector3.Lerp(startPoint, endPoint, i / (float)(numLanes - 1));
			//SpawnLane lane = Instantiate(lanePrefab, position, Quaternion.identity).GetComponent<SpawnLane>();
			SpawnLane lane = Instantiate(lanePrefab, position, Quaternion.identity).GetComponent<SpawnLane>();
			lane.ConfigSpawnPoints(laneOffset);
			lanes[i] = lane;
		}
	}

	public int TotalEmptyLanes {
		get {
			int totalEmpty = 0;
			for(int i=0; i < numLanes; i++)
			{
				if (IsLaneEmpty( i ))
				{
					totalEmpty++;
				}
			}

			return totalEmpty;
		}
	}

	public bool IsLaneEmpty(int lane) {
		return lanes[lane].IsEmpty;
	}

	public int FirstEmptyLane { 
		get {
			for (int i = 0; i < numLanes; i++)
			{
				if (IsLaneEmpty(i))
				{
					return i;
				}
			}

			return 0;
		}
	}

	public int NumberOfEntityType(EntitySpawnConfig entityConfig) {
		int numOfEntity = 0;

		for (int i = 0; i < numLanes; i++)
		{
			if (lanes[i].EntityConfig == entityConfig)
			{
				numOfEntity++;
			}
		}

		return numOfEntity;
	}

	public void SpawnEntity(EntitySpawnConfig entityConfig, int laneNum, EntityMoveDirection direction) {
		lanes[laneNum].SpawnEntity(entityConfig, direction);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Vector3 leftStart = startPoint.position;
		leftStart.x = -laneOffset;
		Vector3 rightStart = endPoint.position;
		rightStart.x = -laneOffset;
		Gizmos.DrawLine(leftStart, rightStart);

		leftStart = startPoint.position;
		leftStart.x = laneOffset;
		rightStart = endPoint.position;
		rightStart.x = laneOffset;
		Gizmos.DrawLine(leftStart, rightStart);
	}
}
