using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour
{
	public float speed = 10;
	[SerializeField]
	SpriteAnimationController leftMove;
	[SerializeField]
	SpriteAnimationController rightMove;
	[SerializeField]
	SpriteAnimationController upMove;
	[SerializeField]
	SpriteAnimationController fall;
	[SerializeField]
	SpriteAnimationController dead;

	public enum FishState
	{
		jumping,
		fall,
		walking,
		dead
	}
	
	[SerializeField]
	FishState state = FishState.jumping;
	AudioClip deadSound;
	GameObject player;
	
	void Start ()
	{
		deadSound = Resources.Load ("Audio/destroy1") as AudioClip;
		player = GameObject.FindWithTag ("Player");
		
		transform.position += transform.forward * 22;
	}
	
	void Update ()
	{
		if (state == FishState.walking)
			Walking ();
	}
	
	public void JumpHighPosition ()
	{
		state = FishState.fall;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		fall.enabled = true;
		collider.enabled = true;
	}
	
	public void Stop ()
	{
		enabled = false;
		animation.enabled = false;
	}
	
	public void TouchDown ()
	{
		state = FishState.walking;

		transform.position = 
			
				new Vector3 (transform.position.x,
								-42f,
								transform.position.z);
	}
	
	void Walking ()
	{
		Vector3 direct = Vector3.Normalize (player.transform.position - transform.position);
		
		speed += 8 * Time.deltaTime;
		
		if (direct.x > 0)
			rightMove.enabled = true;
		else
			leftMove.enabled = true;
		
		transform.position += (Vector3.right * direct.x) * Time.deltaTime * speed;
	}
	
	IEnumerator Dead ()
	{
		string scoreName = string.Empty;
		if (state == FishState.walking) {
			GameManager.AddScore (20);
			scoreName = "Prefab/point020";
		}
		if (state == FishState.fall) {
			GameManager.AddScore (100);
			scoreName = "Prefab/point100";
		}
		
		dead.enabled = true;
		collider.enabled = false;
		state = FishState.dead;

		animation.Stop ();
		
		AudioSource.PlayClipAtPoint (deadSound, Vector3.zero);
		
		yield return new WaitForSeconds( dead.time * 2f - 0.01f);
		
		GameObject score = GameObject.Instantiate (Resources.Load (scoreName)) as GameObject;
		Destroy (score, score.animation ["pointUp@point"].length);
		score.transform.position = gameObject.transform.position;
		Destroy (gameObject.transform.parent.gameObject);
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (collision.gameObject.tag.Equals ("Bullet")) 
			StartCoroutine (Dead ());
	}
}
