using UnityEngine;

public class SpawnHeuristic : MonoBehaviour {

	public LaneManager laneManager;

	[Header("Entity Config")]
	public EntitySpawnConfig[] entityConfigs;

	[SerializeField]
	private int currentLevel;

	public float difficulty;
	private float tweakValue = 3.0f;
	private double tickTimer = 0.0;
	private double updateInterval = 1.0f;
	private float[] probability;

	public void Start()
	{
		probability = new float[entityConfigs.Length + 1];
	}
	
	public int CurrentLevel {
		set {
			currentLevel = value;

			difficulty = 1.0f - (3f / (3f + currentLevel));
		}
	}

	private void getProbabilities() {

		int length = entityConfigs.Length + 1;
		float totalProb = 0f;
		int entityTypeCount = 0;
		int totalCount = 0;

		for (int i=0; i < length; i++)
		{
			if (i == 0)
			{
				probability[i] = 1 - difficulty;
			}
			else
			{
				probability[i] = (1 - Mathf.Abs(difficulty - entityConfigs[i - 1].difficulty));
				entityTypeCount = laneManager.NumberOfEntityType(entityConfigs[i -1 ]);
				totalCount = laneManager.TotalEmptyLanes;
			}

			
			//probability[i] = probability[i] * (tweakValue / (tweakValue + entityTypeCount)) * (5f / (5f + (laneManager.numLanes - totalCount)));
			probability[i] = probability[i] * (tweakValue / (tweakValue + entityTypeCount));

			totalProb += probability[i];
		}

		for(int i=0; i < length; i++)
		{
			
			probability[i] /= totalProb;
		}
	}

	private int selectEntity() {
		int entityNum = -1;

		float randNum = Random.value;
		float currentProb = 0f;


		for(int i=0; i < probability.Length; i++)
		{
			currentProb += probability[i];
			if (randNum <= currentProb)
			{
				entityNum = i - 1;
				return entityNum;
			}
		}
		

		return entityNum;
	}

	private int getEmptyLane()
	{
		int laneIndex;

		laneIndex = Random.Range(0, laneManager.TotalEmptyLanes);

		for (int i = 0; i < laneManager.numLanes; i++)
		{
			if (laneManager.IsLaneEmpty(i))
			{
				if (laneIndex == i)
				{
					return i;
				}
			}
			else
			{
				laneIndex++;
			}
		}

		return -1;
	}


	private void Update()
	{
		int entityNum;
		int laneIndex;
		
		tickTimer += Time.deltaTime;

		if(tickTimer >= updateInterval)
		{
			tickTimer -= updateInterval;

			getProbabilities();

			laneIndex = getEmptyLane();

			if(laneIndex >= 0)
			{
				entityNum = selectEntity();

				if (entityNum >= 0)
				{
					laneManager.SpawnEntity(entityConfigs[entityNum], laneIndex, (EntityMoveDirection)Random.Range(0, 2));
				}
				else
				{
					//Debug.Log("Decided to not spawn");
				}
			}
		}
	}
	
}
