using UnityEngine;

public class Zone : MonoBehaviour {

	public delegate void ObjectZoneEvent(GameObject gameObject);
	public ObjectZoneEvent OnObjectEnter;

	private void OnTriggerEnter(Collider other) {
		OnObjectEnter?.Invoke(other.gameObject);
	}

}
