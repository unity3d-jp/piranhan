using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	public int clearCount = 10;
	public int hp = 3;
	[SerializeField]
	Camera mainCamera;
	private int killCount = 0;
	public bool IsBulletShooted{ get; set;}
	private RandomSpawn spawn;
	
	
	public override void Init ()
	{
		ScoreManager.instance.Reset();
		spawn = FindObjectOfType(typeof(RandomSpawn)) as RandomSpawn;
	}

	public static void DestroyEnemy (int addScorePoint)
	{
		ScoreManager.instance.AddScore (addScorePoint);
		AddKillCount ();
	}

	private static void AddKillCount ()
	{
		instance.killCount += 1;
		// clear
		if (instance.killCount >= instance.spawn.SpawnNumber)
			instance.StartCoroutine (instance.GameClear ());
	}

	public static void Miss ()
	{
		
		Dustbox.instance.StopFishes ();
		
		instance.spawn.enabled = false;
		Dustbox.instance.StopBullet();
		
		instance.hp -= 1;
		
		
		// GameOver
		if (instance.hp <= 0)
			instance.StartCoroutine (instance.GameOver ());
		else
			instance.animation.Play ();

		instance.animation.Play ();
		
	}
	
	public void ResetGame ()
	{
		Destroy (Dustbox.instance.gameObject);
		
		CatController cat = GameObject.FindObjectOfType (typeof(CatController)) as CatController;
		cat.Reset ();

		instance.spawn.spawnCount = instance.killCount ;
		spawn.enabled = true;
		
	}

	public void Fadeout ()
	{
		if (mainCamera != null)
			mainCamera.enabled = true;
	}
	
	public void Fadein ()
	{
		if (mainCamera != null)
			mainCamera.enabled = false;
	}
	
	IEnumerator GameClear ()
	{
		
		RandomSpawn spawn = GameObject.FindObjectOfType (typeof(RandomSpawn)) as RandomSpawn;
		PlayerController controller = GameObject.FindObjectOfType (typeof(PlayerController)) as PlayerController;
		MusicController sound = GameObject.FindObjectOfType (typeof(MusicController))  as MusicController;
		AudioClip clip = Resources.Load ("Audio/bgm-jingle1") as AudioClip;
		CatAnimationController cat = GameObject.FindObjectOfType (typeof(CatAnimationController)) as CatAnimationController;
		
		Dustbox.instance.StopFishes ();
		spawn.enabled = false;
		controller.enabled = false;
		cat.clearAction.enabled = true;
		
		if (sound != null)
			Destroy (sound.gameObject);
		
		AudioSource.PlayClipAtPoint (clip, Vector3.zero);
		
		yield return new WaitForSeconds(clip.length);
		
		Destroy(Dustbox.instance.gameObject);
		Application.LoadLevel ("Clear");
	}

	public IEnumerator GameOver ()
	{
		yield return new WaitForSeconds(2);
		Destroy(Dustbox.instance.gameObject);
		
		MusicController sound = GameObject.FindObjectOfType (typeof(MusicController)) as MusicController;
		if (sound != null)
			Destroy (sound.gameObject);
	
		Application.LoadLevel ("GameOver");
	}
}
