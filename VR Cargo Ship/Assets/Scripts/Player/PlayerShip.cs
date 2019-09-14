using UnityEngine;

public class PlayerShip : MonoBehaviour {

	[Header("Forward Movement")]
	public float baseMoveSpeed = 0f;
	public float baseBoostMoveSpeed = 0f;
	public float baseMoveSpeedAccel = 0.075f;
	private float moveSpeedModifier = 1f;
	private float moveSpeed = 0;

	[Header("Steering Movement")]
	public float baseSteerSpeed = 0f;
	public float baseSteerSpeedAccel = 0.05f;
	public float steerSpeed = 0f;
	private float steerSpeedModifier = 1f;

	[HideInInspector]
	public InputManager inputManager;

	public delegate void PlayerDeathEvent();
	public PlayerDeathEvent OnPlayerDeath;

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
		moveSpeedModifier = preset.moveSpeedModifier;
		steerSpeedModifier = preset.steerSpeedModifier;
	}

	public void Update() {
		Move();
		Steer();
	}

	private void Move() {
		float targetMoveSpeed = inputManager.IsBoosting ? baseBoostMoveSpeed : baseMoveSpeed;
		moveSpeed = Mathf.MoveTowards(moveSpeed, targetMoveSpeed, baseMoveSpeedAccel);
	
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	private void Steer() {
		float targetSteerSpeed = 0f;

		if (inputManager.IsLeft) {
			targetSteerSpeed = -baseSteerSpeed;
		}
		if (inputManager.IsRight) {
			targetSteerSpeed = baseSteerSpeed;
		}

		steerSpeed = Mathf.MoveTowards(steerSpeed, targetSteerSpeed, baseSteerSpeedAccel);

		Vector3 pos = transform.position;
		pos.x += steerSpeed * Time.deltaTime;
		transform.position = pos;
	}

	public void Damage(int amount) {
		crates = Mathf.Max(crates - amount, 0);

		if (crates == 0) {
			OnPlayerDeath?.Invoke();
			Destroy(gameObject);
		}
	}

}
