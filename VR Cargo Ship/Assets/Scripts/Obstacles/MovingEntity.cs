using UnityEngine;

public class MovingEntity : MonoBehaviour {

	public float moveSpeed = 0f;

	private Vector3 moveDir;
	private float moveDist;
	private Vector3 startPosition;
	private float currentFactor;

	[HideInInspector]
	public EntityMoveDirection direction = EntityMoveDirection.Left;

	private bool isMoving = false;

	public void OrientModel() {
		if (direction == EntityMoveDirection.Right) {
			transform.Rotate(Vector3.up, -90);
		} else {
			transform.Rotate(Vector3.up, 90);
		}
	}

	public void StartMoving(Vector3 from, Vector3 to) {
		isMoving = true;

		moveDir = to - from;
		moveDist = moveDir.magnitude;
		moveDir = Vector3.Normalize(moveDir);

		startPosition = transform.position;
	}

	private void Update() {
		if (isMoving) {
			Move();
		}
	}

	private void Move() {
		currentFactor += moveSpeed * Time.deltaTime;
		if (currentFactor > moveDist)
		{
			Destroy(gameObject);
			return;
		}

		transform.position = startPosition + moveDir * currentFactor;
	}

}
