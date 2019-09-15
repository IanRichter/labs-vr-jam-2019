using UnityEngine;

public class Obstacle : MonoBehaviour {

	public int damage = 0;
	public bool isDestroyable = true;

	public delegate void EntityEvent(Obstacle entity);
	public EntityEvent OnDestroyed;

	private bool hasHitPlayer = false;

	[HideInInspector]
	public GameObject entityDeadPrefab;

	public void DestroyObject() {
		Destroy(gameObject);
	}

	private void Die() {
		OnDestroyed?.Invoke(this);
		GameObject deadObject = Instantiate(entityDeadPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	public void OnTriggerEnter(Collider other) {
		if (hasHitPlayer) {
			return;
		}

		PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
		if (playerShip) {
			playerShip.Damage(damage);
			hasHitPlayer = true;

			if (isDestroyable) {
				Die();
			}
		}
	}

}
