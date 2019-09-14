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

	}

}
