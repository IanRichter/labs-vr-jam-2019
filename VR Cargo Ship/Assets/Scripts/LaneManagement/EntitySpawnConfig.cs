using UnityEngine;

[CreateAssetMenu(fileName = "Create Entity Config")]
public class EntitySpawnConfig : ScriptableObject {

	[Header("Entity")]
	public GameObject entityPrefab;

	[Header("Properties")]
	public float difficulty = 0f;

}
