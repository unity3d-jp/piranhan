using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	private CatController controller;
	private ShotController shot;
	
	void Start ()
	{
		controller = GetComponent<CatController> ();
		shot = GetComponent<ShotController> ();
	}
	
	void Update ()
	{
		if (Input.GetAxis ("Vertical") > 0)
			controller.MoveUp ();
		else if (Input.GetAxis ("Horizontal") < 0)
			controller.MoveLeft ();
		else if (Input.GetAxis ("Horizontal") > 0)
			controller.MoveRight ();

		if (Input.GetButtonDown ("Fire1"))
			shot.Shoot ();

		if (Input.GetKeyDown (KeyCode.LeftShift))
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}
}
