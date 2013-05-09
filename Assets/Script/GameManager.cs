using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	public int clearCount = 10;
	public int hp = 3;
	[SerializeField]
	Camera mainCamera;
	private int killCount = 0;
	
	public override void Init ()
	{
		mainCamera = Camera.mainCamera;
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
		if (instance.killCount >= instance.clearCount)
			instance.StartCoroutine (instance.GameClear ());
	}

	public static void Miss ()
	{
		
		Dustbox.instance.StopFishes ();
		RandomSpawn spawn = GameObject.FindObjectOfType (typeof(RandomSpawn)) as RandomSpawn;
		
		spawn.enabled = false;
		
		
		instance.hp -= 1;

		spawn.spawnCount = instance.killCount - 1;
		
		
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

		RandomSpawn spawn = GameObject.FindObjectOfType (typeof(RandomSpawn)) as RandomSpawn;
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
		
		Dustbox.instance.StopFishes ();
		spawn.enabled = false;
		controller.enabled = false;
		
		if (sound != null)
			Destroy (sound.gameObject);
		
		AudioSource.PlayClipAtPoint (clip, Vector3.zero);
		
		yield return new WaitForSeconds(clip.length);
		
		Application.LoadLevel ("Clear");
	}

	public IEnumerator GameOver ()
	{
		yield return new WaitForSeconds(2);
		
		MusicController sound = GameObject.FindObjectOfType (typeof(MusicController)) as MusicController;
		if (sound != null)
			Destroy (sound.gameObject);
	
		Application.LoadLevel ("GameOver");
	}
}
