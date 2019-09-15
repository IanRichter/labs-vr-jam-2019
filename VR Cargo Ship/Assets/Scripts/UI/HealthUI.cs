using UnityEngine;

public class HealthUI : MonoBehaviour {

	public GameObject[] healthDisplay;


	private void Start() {
		if (healthDisplay == null) {
			healthDisplay = new GameObject[0];
		}	
	}

	public void SetHealth(int health) {
		for (int i = 0; i < healthDisplay.Length; i++) {
			healthDisplay[i].SetActive(i < health);
		}
	}

}
