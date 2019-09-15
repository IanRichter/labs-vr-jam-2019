using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingEntity : MonoBehaviour
{
	public float sinkAccel = 0.01f;
	public float rotationAccel = 200.0f;

	private float currentSpeed = 0.0f;
	private float currentRotation = 0.0f;
	private Quaternion originalRotation;
	private Vector3 originalRight;

    void Start()
    {
		originalRotation = transform.rotation;
		originalRight = transform.right;
	}

    void Update()
    {
		currentSpeed += Time.deltaTime * sinkAccel;
		currentRotation = Mathf.MoveTowards(currentRotation, 80.0f, Time.deltaTime * rotationAccel);

		Vector3 position = transform.position;
		position.y -= currentSpeed;
		transform.position = position;

		transform.rotation = originalRotation * Quaternion.AngleAxis(currentRotation, originalRight);

		if (position.y < 0.0f)
		{
			Destroy(gameObject);
		}
    }
}
