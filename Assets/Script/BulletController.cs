using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	
	[HideInInspector]
	public Vector3 direction = Vector3.up;
	public float speed = 4;
	private readonly static float margin = 0.02f;
	
	void Start ()
	{
		GameObject dust = GameObject.Find ("dustbox") as GameObject;
		if (dust == null)
			dust = new GameObject ("dustbox");
		transform.parent = dust.transform;
		
		AudioClip shootAudio = Resources.Load ("Audio/shot1") as AudioClip;
		AudioSource.PlayClipAtPoint (shootAudio, Vector3.zero);
	}
	
	void Update ()
	{
		
		if (Time.timeScale == 0)
			return;
		
		transform.position += direction * speed;
		
		Vector3 bulletScreenPos = Camera.mainCamera.WorldToViewportPoint (transform.position);
		if (bulletScreenPos.x < 0 - margin || bulletScreenPos.x > 1 + margin || 
			bulletScreenPos.y < 0 - margin || bulletScreenPos.y > 1 + margin)
			Destroy (gameObject);
	}
	
	public void Stop ()
	{
		enabled = false;
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (!collision.CompareTag ("Player"))
			Destroy (gameObject);
	}

}
