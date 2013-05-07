using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int clearCount = 10;
	
	[HideInInspector]
	public int score = 0;

	public int hp = 3;
	
	[SerializeField]
	Camera mainCamera;

	private int killCount = 0;
	
	void Awake()
	{
		mainCamera = Camera.mainCamera;
		PlayerPrefs.SetInt("score", 0);
	}

	
	public static void AddScore(int point)
	{
		GameManager manager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
		if( manager == null)
			return;

		manager.score += point;
		PlayerPrefs.SetInt("score", manager.score);
		
		int highScore = Mathf.Max( manager.score, PlayerPrefs.GetInt("highscore"));
		PlayerPrefs.SetInt("highscore", highScore );

		manager.killCount += 1;
		
		// clear
		if( manager.killCount >= manager.clearCount )
			manager.StartCoroutine(manager.GameClear());
	}
	
	public static void Miss()
	{
		GameObject dustbox = GameObject.Find("dustbox") as GameObject;
		dustbox.BroadcastMessage("Stop", SendMessageOptions.DontRequireReceiver);
		RandomSpawn spawn = GameObject.FindObjectOfType(typeof(RandomSpawn)) as RandomSpawn;
		
		spawn.enabled = false;
		
		
		GameManager manager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
		manager.hp -= 1;

		spawn.spawnCount = manager.killCount - 1;
		
		
		// GameOver
		if( manager.hp <= 0)
			manager.StartCoroutine(manager.GameOver());
		else
			manager.animation.Play();

		manager.animation.Play();
		
	}
	
	public void ResetGame()
	{
		GameObject dustbox = GameObject.Find("dustbox") as GameObject;
		Destroy (dustbox);
		
		CatController cat = GameObject.FindObjectOfType(typeof(CatController)) as CatController;
		cat.Reset();

		RandomSpawn spawn = GameObject.FindObjectOfType(typeof(RandomSpawn)) as RandomSpawn;
		spawn.enabled = true;
		
	}

	public void Fadeout()
	{
		if( mainCamera != null)
			mainCamera.enabled = true;
	}
	
	public void Fadein()
	{
		if( mainCamera != null)
			mainCamera.enabled = false;
	}
	
	IEnumerator GameClear()
	{
		GameObject dustbox = GameObject.Find("dustbox") as GameObject;
		dustbox.BroadcastMessage("Stop", SendMessageOptions.DontRequireReceiver);
		RandomSpawn spawn = GameObject.FindObjectOfType(typeof(RandomSpawn)) as RandomSpawn;
		spawn.enabled = false;
		
		Controller controller = GameObject.FindObjectOfType(typeof( Controller )) as Controller;
		controller.enabled = false;
		
		MusicController sound = GameObject.FindObjectOfType(typeof( MusicController) )  as MusicController;
		if( sound != null)
			Destroy( sound.gameObject );
		
		AudioClip clip = Resources.Load("Audio/bgm-jingle1") as AudioClip;
		AudioSource.PlayClipAtPoint(clip, Vector3.zero);
		
		yield return new WaitForSeconds(clip.length);

		Application.LoadLevel("Clear");
		
	}

	public IEnumerator GameOver()
	{
		yield return new WaitForSeconds(2);
		
		MusicController sound = GameObject.FindObjectOfType(typeof( MusicController) ) as MusicController ;
		if( sound != null)
			Destroy( sound.gameObject );
	
		Application.LoadLevel("GameOver");
		yield return null;
	}
}
