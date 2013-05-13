using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	
	[HideInInspector]
	public Vector3 direction = Vector3.up;
	public float speed = 4f;
	private const float margin = 0.02f;
	private bool isDestroy = false;
	
	void Start ()
	{
		transform.parent = Dustbox.instance.transform;
		
		AudioClip shootAudio = Resources.Load ("Audio/shot1") as AudioClip;
		AudioSource.PlayClipAtPoint (shootAudio, Vector3.zero);
	}
	
	void Update ()
	{
		
		if( isDestroy ) 
			Destroy (gameObject);
		
		
		transform.Translate (direction * speed * 60 * Time.deltaTime, Space.World);
		
		Vector3 bulletScreenPos = Camera.mainCamera.WorldToViewportPoint (transform.position);
		if (bulletScreenPos.x < 0 - margin || bulletScreenPos.x > 1 + margin || 
			bulletScreenPos.y < 0 - margin || bulletScreenPos.y > 1 + margin)
			Destroy (gameObject);
	}
	
	void OnDestroy()
	{
		GameManager.instance.IsBulletShooted = false;
	}
	
	public void Stop ()
	{
		enabled = false;
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (!collision.CompareTag ("Player"))
			isDestroy = true;
	}

}
