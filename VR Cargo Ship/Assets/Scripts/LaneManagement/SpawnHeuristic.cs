using UnityEngine;

public class SpawnHeuristic : MonoBehaviour {

	public LaneManager laneManager;

	[Header("Entity Config")]
	public EntitySpawnConfig[] entityConfigs;

	[SerializeField]
	private int currentLevel;

	[SerializeField]
	private float difficultyRamp = 10.0f;

	[SerializeField]
	private float probabilityRangeFactor = 2.0f;

	public float difficulty;
	private float tweakValue = 3.0f;
	private double tickTimer = 0.0;
	private double updateInterval = 1.0f;
	private float[] probabilities;

	public void Start()
	{
		probabilities = new float[entityConfigs.Length + 1];
	}
	
	public int CurrentLevel {
		set {
			currentLevel = value;

			difficulty = 1.0f - (difficultyRamp / (difficultyRamp + currentLevel));
		}
	}

	private void getProbabilities() {

		int length = entityConfigs.Length + 1;
		float totalProb = 0f;
		int entityTypeCount = 0;
		int totalCount = 0;

		for (int i=0; i < length; i++)
		{
			float probability = 0.0f;

			if (i == 0)
			{
				probability = 1 - difficulty;
			}
			else
			{
				probability = Mathf.Max(1 - Mathf.Abs(difficulty - entityConfigs[i - 1].difficulty) * probabilityRangeFactor, 0.0f);
				entityTypeCount = laneManager.NumberOfEntityType(entityConfigs[i -1 ]);
				totalCount = laneManager.TotalEmptyLanes;
			}


			//probability = probability * (tweakValue / (tweakValue + entityTypeCount)) * (5f / (5f + (laneManager.numLanes - totalCount)));
			//probability = probability * (tweakValue / (tweakValue + entityTypeCount));

			totalProb += probability;
			probabilities[i] = probability;
		}
		
		for (int i=0; i < length; i++)
		{
			probabilities[i] /= totalProb;
		}
	}

	private int selectEntity() {
		int entityNum = -1;

		float randNum = Random.value;
		float currentProb = 0f;


		for(int i=0; i < probabilities.Length; i++)
		{
			currentProb += probabilities[i];
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
