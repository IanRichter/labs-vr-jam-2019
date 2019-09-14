using UnityEngine;

public class MovingEntity : MonoBehaviour {

	public float moveSpeed = 0f;

	[HideInInspector]
	public EntityMoveDirection direction = EntityMoveDirection.Left;

	private bool isMoving = true;
	

	public void OrientModel() {
		if (direction == EntityMoveDirection.Right) {
			transform.Rotate(Vector3.forward, 90);
		}
	}

	public void StartMoving() {
		isMoving = true;
	}

	private void Update() {
		if (isMoving) {
			Move();
		}
	}

	private void Move() {
		transform.position = transform.position + (Vector3.forward * moveSpeed * Time.deltaTime);
	}

}
