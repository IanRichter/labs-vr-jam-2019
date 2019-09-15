using UnityEngine;

public class PlayerShip : MonoBehaviour {

	[Header("Forward Movement")]
	public float baseMoveSpeed = 0f;
	public float baseBoostMoveSpeed = 0f;
	public float baseMoveSpeedAccel = 0.075f;
	//private float moveSpeedModifier = 1f;
	private float moveSpeed = 0;

	[Header("Steering Movement")]
	public float baseSteerAngle = 20.0f;
	public float baseSteerAngleAccel = 1.0f;
	public float steerAngle = 0f;

	[Header("Tilting")]
	public float tiltAmount = 0.3f;
	public float tiltAccel = 1.0f;
	private float tiltAngle = 0f;

	[HideInInspector]
	public InputManager inputManager;

	public delegate void PlayerDeathEvent();
	public PlayerDeathEvent OnPlayerDeath;

	[HideInInspector]
	public float mapEdge = 1f;

	// Cargo
	private int crates = 0;
	public int Crates {
		get {
			return crates;
		}
	}

	[Header("Particle Systems")]
	public ParticleSystem crateDestroyParticleSystem;
	
	public void ConfigFromPreset(PlayerShipPreset preset) {
		//moveSpeedModifier = preset.moveSpeedModifier;
		//steerSpeedModifier = preset.steerSpeedModifier;
	}
	
	public void Update() {
		Move();
		Steer();

		Quaternion forward = Quaternion.AngleAxis(steerAngle, Vector3.up);
		transform.rotation = forward * Quaternion.AngleAxis(tiltAngle * tiltAmount, forward * Vector3.forward);
		Vector3 forwardVector = (transform.rotation * Vector3.forward);
		forwardVector.y = 0.0f;

		Vector3 position = transform.position;
		position += forwardVector * moveSpeed * Time.deltaTime;
		position.x = Mathf.Clamp(position.x, -mapEdge, mapEdge);
		transform.position = position;
	}

	private void Move() {
		float targetMoveSpeed = inputManager.IsBoosting ? baseBoostMoveSpeed : baseMoveSpeed;
		moveSpeed = Mathf.MoveTowards(moveSpeed, targetMoveSpeed, Time.deltaTime * baseMoveSpeedAccel);
	}

	private void Steer() {
		float targetSteerAngle = 0f;
		if (inputManager.IsLeft)
		{
			targetSteerAngle = -baseSteerAngle;
		}
		if (inputManager.IsRight)
		{
			targetSteerAngle = baseSteerAngle;
		}

		steerAngle = Mathf.MoveTowards(steerAngle, targetSteerAngle, Time.deltaTime * baseSteerAngleAccel);

		float targetSteerAngleDelta = (targetSteerAngle - steerAngle) * tiltAmount;
		tiltAngle = Mathf.MoveTowards(tiltAngle, targetSteerAngleDelta, tiltAccel * Time.deltaTime);
	}

	public void Damage(int amount) {
		crates = Mathf.Max(crates - amount, 0);

		if (crates == 0) {
			OnPlayerDeath?.Invoke();
			Destroy(gameObject);
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(new Ray(transform.position, transform.rotation * Vector3.forward));
	}
}
