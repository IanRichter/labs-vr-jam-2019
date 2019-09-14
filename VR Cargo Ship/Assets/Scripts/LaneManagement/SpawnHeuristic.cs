using UnityEngine;

public class SpawnHeuristic : MonoBehaviour {

	public LaneManager laneManager;

	[Header("Entity Config")]
	public EntitySpawnConfig[] entityConfigs;

	private float difficulty;
	private float tweakValue = 3.0f;
	private double tickTimer = 0.0;
	private double updateInterval = 1.0f;
	private float[] probability;

	public void Start()
	{
		probability = new float[entityConfigs.Length];
	}
	

	private int currentLevel;
	public int CurrentLevel {
		set {
			currentLevel = value;

			difficulty = 1.0f - (tweakValue / tweakValue - currentLevel);
		}
	}

	private void getProbabilities() {

		int length = entityConfigs.Length;
		float totalProb = 0f;

		for (int i=0; i < length; i++)
		{
			probability[i] = (1 - Mathf.Abs(difficulty - entityConfigs[i].difficulty));
			totalProb += probability[i];
		}

		for(int i=0; i < length; i++)
		{
			probability[i] /= totalProb;

			int entityTypeCount = laneManager.NumberOfEntityType(entityConfigs[i]);
			int totalCount = laneManager.TotalEmptyLanes;
			probability[i] = probability[i] * (tweakValue / (tweakValue + entityTypeCount)) * (tweakValue / (tweakValue + (laneManager.numLanes - totalCount))) ;
		}
	}

	private int selectEntity() {
		int entityNum = -1;

		float randNum = Random.value;
		float currentProb = 0f;


		for(int i=0; i < probability.Length; i++)
		{
			currentProb += probability[i];
			if (randNum < currentProb)
			{
				entityNum = i;
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
					Debug.Log("Decided to not spawn");
				}
			}
		}
	}





}
