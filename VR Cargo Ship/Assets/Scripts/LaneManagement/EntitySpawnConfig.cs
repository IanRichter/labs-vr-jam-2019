using UnityEngine;

[CreateAssetMenu(fileName = "Create Entity Config")]
public class EntitySpawnConfig : ScriptableObject {

	[Header("Entity")]
	public GameObject entityPrefab;
	public GameObject entityDeadPrefab;

	[Header("Properties")]
	public float difficulty = 0f;

}
