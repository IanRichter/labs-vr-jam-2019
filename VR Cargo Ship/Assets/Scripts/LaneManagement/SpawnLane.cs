using UnityEngine;

public class SpawnLane : MonoBehaviour {

	public Transform leftBorder;
	public Transform rightBorder;

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

	
	
	public void SpawnEntity(EntitySpawnConfig entityConfig, EntityMoveDirection direction) {
		if (entity) {
			return;
		}
		
		this.entityConfig = entityConfig;

		Vector3 spawnPos = direction == EntityMoveDirection.Left ? leftBorder.position : rightBorder.position;
		Vector3 endPos = direction == EntityMoveDirection.Left ? rightBorder.position : leftBorder.position;

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

}
