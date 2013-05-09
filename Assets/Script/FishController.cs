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
		Jumping,
		Fall,
		Walking,
		Dead
	}
	
	[SerializeField]
	FishState state = FishState.Jumping;
	AudioClip deadSound;
	GameObject player;
	
	void Start ()
	{
		deadSound = Resources.Load ("Audio/destroy1") as AudioClip;
		player = GameObject.FindWithTag ("Player");
		
		transform.Translate (transform.forward * 22);
	}
	
	void Update ()
	{
		if (state == FishState.Walking)
			Walking ();
	}
	
	public void JumpHighPosition ()
	{
		state = FishState.Fall;
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
		state = FishState.Walking;

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
		if (state == FishState.Walking) {
			GameManager.AddScore (20);
			scoreName = "Prefab/point020";
		}
		if (state == FishState.Fall) {
			GameManager.AddScore (100);
			scoreName = "Prefab/point100";
		}
		
		dead.enabled = true;
		collider.enabled = false;
		state = FishState.Dead;

		animation.Stop ();
		
		AudioSource.PlayClipAtPoint (deadSound, Vector3.zero);
		
		yield return new WaitForSeconds( dead.time * 2f - 0.01f);
		
		GameObject score = GameObject.Instantiate (Resources.Load (scoreName)) as GameObject;
		Destroy (score, score.animation ["pointUp@point"].length);
		score.transform.position = gameObject.transform.position;
		Destroy (transform.parent.gameObject);
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (collision.CompareTag ("Bullet")) 
			StartCoroutine (Dead ());
	}
}
