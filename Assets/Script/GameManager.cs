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
		GameManager manager = GameManager.instance;
		manager.killCount += 1;
		// clear
		if (manager.killCount >= manager.clearCount)
			manager.StartCoroutine (manager.GameClear ());
	}

	public static void Miss ()
	{
		
		Dustbox.instance.StopFishes ();
		RandomSpawn spawn = GameObject.FindObjectOfType (typeof(RandomSpawn)) as RandomSpawn;
		
		spawn.enabled = false;
		
		
		GameManager manager = GameManager.instance;
		manager.hp -= 1;

		spawn.spawnCount = manager.killCount - 1;
		
		
		// GameOver
		if (manager.hp <= 0)
			manager.StartCoroutine (manager.GameOver ());
		else
			manager.animation.Play ();

		manager.animation.Play ();
		
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
		Dustbox.instance.StopFishes ();
		RandomSpawn spawn = GameObject.FindObjectOfType (typeof(RandomSpawn)) as RandomSpawn;
		spawn.enabled = false;
		
		PlayerController controller = GameObject.FindObjectOfType (typeof(PlayerController)) as PlayerController;
		controller.enabled = false;
		
		MusicController sound = GameObject.FindObjectOfType (typeof(MusicController))  as MusicController;
		if (sound != null)
			Destroy (sound.gameObject);
		
		AudioClip clip = Resources.Load ("Audio/bgm-jingle1") as AudioClip;
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
