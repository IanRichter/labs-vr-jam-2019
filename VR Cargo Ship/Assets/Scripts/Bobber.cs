using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
	public float bobAmount = 0.02f;
	public float bobSpeed = 2.0f;

	private float currentTime = 0.0f;
	private Transform transform;
	private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
		transform = GetComponent<Transform>();
		originalPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		currentTime += Time.deltaTime;

		Vector3 position = transform.position;
		position.y = originalPosition.y + Mathf.Sin(currentTime * bobSpeed) * bobAmount;
		transform.position = position;

	}
}
