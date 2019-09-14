using UnityEngine;

public class SpawnLane : MonoBehaviour {

	private float laneOffset;
	private Vector3 leftPos;
	private Vector3 rightPos;

	private Obstacle entity;
	
	private EntitySpawnConfig entityConfig;
	public EntitySpawnConfig EntityConfig {
		get {
			return entityConfig;
		}
	}

	public bool IsEmpty {
		get {
			return !entity;
		}
	}

	public void ConfigSpawnPoints(float offset)
	{
		laneOffset = offset;
		leftPos = new Vector3(-laneOffset, 0.0f, 0.0f);
		rightPos = new Vector3(laneOffset, 0.0f, 0.0f);
	}
	
	public void SpawnEntity(EntitySpawnConfig entityConfig, EntityMoveDirection direction) {
		if (entity) {
			return;
		}
		
		this.entityConfig = entityConfig;

		Vector3 spawnPos = (direction == EntityMoveDirection.Left) ? leftPos : rightPos;
		Vector3 endPos = (direction == EntityMoveDirection.Left) ? rightPos : leftPos;

		GameObject newObject = Instantiate(entityConfig.entityPrefab, spawnPos, Quaternion.identity);

		// Obstacle
		entity = newObject.GetComponent<Obstacle>();
		entity.OnDestroyed += EntityDestroyHandler;

		// MovingEntity
		MovingEntity movingEntity = newObject.GetComponent<MovingEntity>();
		movingEntity.direction = direction;
		movingEntity.OrientModel();
		movingEntity.StartMoving(spawnPos, endPos);
	}
	
	private void EntityDestroyHandler(Obstacle entity) {
		this.entity.OnDestroyed -= EntityDestroyHandler;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(leftPos, 0.1f);
		Gizmos.DrawWireSphere(rightPos, 0.1f);
	}

}
