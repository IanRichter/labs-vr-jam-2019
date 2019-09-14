using UnityEngine;

public class SpawnLane : MonoBehaviour {

	public Zone leftZone;
	public Zone rightZone;

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

	private void Start() {
		leftZone.OnObjectEnter += ZoneEnterHandler;
		rightZone.OnObjectEnter += ZoneEnterHandler;
	}
	
	public void SpawnEntity(EntitySpawnConfig entityConfig, EntityMoveDirection direction) {
		if (entity) {
			return;
		}
		
		this.entityConfig = entityConfig;

		Zone spawnZone = direction == EntityMoveDirection.Left ? leftZone : rightZone;
		GameObject newObject = Instantiate(entityConfig.entityPrefab, spawnZone.transform.position, Quaternion.identity);

		// Obstacle
		entity = newObject.GetComponent<Obstacle>();
		entity.OnDestroyed += EntityDestroyHandler;

		// MovingEntity
		MovingEntity movingEntity = newObject.GetComponent<MovingEntity>();
		movingEntity.direction = direction;
		movingEntity.OrientModel();
		movingEntity.StartMoving();
	}

	private void ZoneEnterHandler(GameObject other) {
		if (other == entity.gameObject) {
			entityConfig = null;
			Destroy(entity);
		}
	}

	private void EntityDestroyHandler(Obstacle entity) {
		this.entity.OnDestroyed -= EntityDestroyHandler;
	}

}
