using UnityEngine;

public class SpawnLane : MonoBehaviour {

	public Zone leftZone;
	public Zone rightZone;

	private GameObject activeObject;
	public GameObject ActiveObject {
		get {
			return activeObject;
		}
	}
	
	private EntitySpawnConfig entityConfig;
	public EntitySpawnConfig EntityConfig {
		get {
			return entityConfig;
		}
	}


	public bool IsEmpty {
		get {
			return !activeObject;
		}
	}

	public void SpawnEntity(EntitySpawnConfig entityConfig, EntityMoveDirection direction) {
		if (activeObject) {
			return;
		}

		this.entityConfig = entityConfig;

		Zone spawnZone = direction == EntityMoveDirection.Left ? leftZone : rightZone;
		activeObject = Instantiate(entityConfig.entityPrefab, spawnZone.transform.position, Quaternion.identity);

		MovingEntity movingEntity = activeObject.GetComponent<MovingEntity>();
		if (movingEntity) {
			movingEntity.direction = direction;
			movingEntity.OrientModel();
			movingEntity.StartMoving();
		}
	}

}
