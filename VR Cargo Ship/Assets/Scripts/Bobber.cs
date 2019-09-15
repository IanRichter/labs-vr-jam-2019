using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
	public float bobAmount = 0.01f;
	public float bobSpeed = 3.0f;
	public float heightAdjustSpeed = 2.0f;
	public float tiltFactor = 1.0f;
	public float tiltSpeed = 1.0f;

	private float currentTime = 0.0f;
	private Transform transform;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private float currentHeightOffset;
	private Vector3 currentUpNormal;
	private Vector3 targetUpNormal;
	
	public WaterSimulator waterSimulator;

	public void Config(WaterSimulator waterSimulator)
	{
		this.waterSimulator = waterSimulator;
	}

	// Start is called before the first frame update
	void Start()
    {
		transform = GetComponent<Transform>();
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		currentHeightOffset = waterSimulator.GetHeightAtWorldPosition(originalPosition);
		currentUpNormal = Vector3.up;
	}

    // Update is called once per frame
    void Update()
    {
		currentTime += Time.deltaTime;

		Vector3 position = transform.position;

		float bobOffset = Mathf.Sin(currentTime * bobSpeed) * bobAmount;

		float targetHeightOffset = waterSimulator.GetHeightAtWorldPosition(position);
		currentHeightOffset += (targetHeightOffset - currentHeightOffset) * Time.deltaTime * heightAdjustSpeed;

		targetUpNormal = waterSimulator.GetNormalAtWorldPosition(position);
		targetUpNormal.x *= tiltFactor;
		targetUpNormal.z *= tiltFactor;
		targetUpNormal = Vector3.Normalize(targetUpNormal);

		currentUpNormal = Vector3.Normalize(Vector3.MoveTowards(currentUpNormal, targetUpNormal, Time.deltaTime * tiltSpeed));
		transform.rotation = Quaternion.FromToRotation(Vector3.up, currentUpNormal) * originalRotation;

		position.y = originalPosition.y + currentHeightOffset + bobOffset;
		transform.position = position;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, targetUpNormal);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, currentUpNormal);
	}
}
