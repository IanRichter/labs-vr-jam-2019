using UnityEngine;

public class SpawnLane : MonoBehaviour {

	public Zone leftZone;
	public Zone rightZone;

	private MovingEntity movingEntity;
	
	private EntitySpawnConfig entityConfig;
	public EntitySpawnConfig EntityConfig {
		get {
			return entityConfig;
		}
	}


	public bool IsEmpty {
		get {
			return !movingEntity;
		}
	}

	private void Start() {
		
	}

	public void SpawnEntity(EntitySpawnConfig entityConfig, EntityMoveDirection direction) {
		if (movingEntity) {
			return;
		}

		this.entityConfig = entityConfig;

		Zone spawnZone = direction == EntityMoveDirection.Left ? leftZone : rightZone;
		movingEntity = Instantiate(entityConfig.entityPrefab, spawnZone.transform.position, Quaternion.identity).GetComponent<MovingEntity>();
	}

	private void ZoneEnterHandler(Collider other) {

	}

}
