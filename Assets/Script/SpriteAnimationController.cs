using UnityEngine;
using System.Collections;

public class SpriteAnimationController : MonoBehaviour
{
	
	
	[Range(0.1f, 1f)]
	public float time = 0.3f;
	[SerializeField]
	string format = string.Empty;
	private int spriteNo = 0;
	private Texture[] sprite = null;
	
	void Awake ()
	{
		sprite = new Texture[2];
		for (int i=0; i<2; i++) {
			string spriteName = string.Format ("Texture/" + format, i + 1);
			sprite [i] = Resources.Load (spriteName, typeof(Texture)) as Texture;
		}
	}

	void OnEnable ()
	{
		SpriteAnimationController[] animations = GetComponents<SpriteAnimationController> ();
		foreach (SpriteAnimationController anim in animations) {
			if (anim != this)
				anim.enabled = false;
		}
		
		StartCoroutine (UpdateSprite ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	IEnumerator UpdateSprite ()
	{
		while (true) {
			renderer.material.mainTexture = sprite [spriteNo];
			spriteNo = (spriteNo == 0) ? 1 : 0;
			
			yield return new WaitForSeconds(time);
		}
	}
}
