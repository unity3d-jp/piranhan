using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
	[SerializeField]
	GameManager manager;

	[SerializeField]
	GameObject fishPrefab;
	
	[HideInInspector]
	public int spawnCount = 0;

	[SerializeField]
	Transform left = null, right = null;
	
	[SerializeField]
	StageRecodeObject stageRecode;
	
	public int SpawnNumber { get{ return stageRecode.stageRecodes.Length; }}

	void OnDrawGizmosSelected ()
	{
		if (left == null || right == null)
			return;
		
		Gizmos.DrawLine (left.position, right.position);
	}

	void OnEnable ()
	{
		StartCoroutine (SpawnLoop ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	IEnumerator SpawnLoop ()
	{
		while (spawnCount < stageRecode.stageRecodes.Length) {
			yield return new WaitForSeconds (stageRecode.stageRecodes[spawnCount].secWait);
			Spawn ();
			spawnCount ++;
		}
	}
	
	public void Spawn ()
	{
		// spawn limit
		if (SpawnNumber <= spawnCount)
			return;
		
		float position = stageRecode.stageRecodes[spawnCount].x;

		if( position == 0 )
			position = Random.Range (left.transform.position.x, right.transform.position.x);
		
		GameObject fish = GameObject.Instantiate (fishPrefab) as GameObject;
		fish.transform.position = transform.position + Vector3.right * (position - 120) + Vector3.forward * 20;
		fish.transform.parent = Dustbox.instance.transform;
	}
	
}


