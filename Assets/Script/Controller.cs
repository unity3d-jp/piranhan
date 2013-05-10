using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	private CatController controller;
	private ShotController shot;
	private VirtualPad vpad;
	
	void Start ()
	{
		controller = GetComponent<CatController> ();
		shot = GetComponent<ShotController> ();
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
		vpad = FindObjectOfType(typeof(VirtualPad)) as VirtualPad;
#endif
	}
	
	void Update ()
	{
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
		if (vpad.up)
			controller.MoveUp ();
		else if (vpad.left)
			controller.MoveLeft ();
		else if (vpad.right)
			controller.MoveRight ();
		
		if (vpad.trigger)
			shot.Shoot ();
#else
		if (Input.GetAxis ("Vertical") > 0)
			controller.MoveUp ();
		else if (Input.GetAxis ("Horizontal") < 0)
			controller.MoveLeft ();
		else if (Input.GetAxis ("Horizontal") > 0)
			controller.MoveRight ();

		if (Input.GetButtonDown ("Fire1"))
			shot.Shoot ();
#endif

		if (Input.GetKeyDown (KeyCode.LeftShift))
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}
}
