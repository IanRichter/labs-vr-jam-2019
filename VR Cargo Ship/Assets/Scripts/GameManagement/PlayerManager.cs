using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public Zone finishZone;

	public delegate void ShipEvent();
	public ShipEvent OnReachFinish;

	private PlayerShip playerShip;


	private void Start() {
		finishZone.OnObjectEnter += FinishHandler;
	}

	public void SpawnShip(PlayerShip ship) {
		playerShip = ship;
	}

	private void FinishHandler(GameObject gameObject) {
		if (gameObject != playerShip) {
			return;
		}

		OnReachFinish?.Invoke();
	}

}
