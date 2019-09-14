using UnityEngine;

public class Obstacle : MonoBehaviour {

	public int damage = 0;
	public bool isDestroyable = true;

	public delegate void EntityEvent(Obstacle entity);
	public EntityEvent OnDestroyed;


	public void DestroyObject() {
		OnDestroyed?.Invoke(this);
		Destroy(gameObject);
	}

	public void OnTriggerEnter(Collider other) {
		PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
		if (playerShip) {
			playerShip.Damage(damage);
			
			if (isDestroyable) {
				DestroyObject();
			}
		}
	}

}
