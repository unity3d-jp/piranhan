using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CatController))]
public class Controller : MonoBehaviour {

	CatController controller;
	ShotController shot;
	
	// Use this for initialization
	void Start () {
		
		controller = GetComponent<CatController>();
		shot = GetComponent<ShotController>();
	
	}
	
	void Update()
	{
		
		
		if( Input.GetKey( KeyCode.UpArrow))
			controller.MoveUp();
		else  if( Input.GetKey(KeyCode.LeftArrow ))
			controller.MoveLeft();
		else if(  Input.GetKey(KeyCode.RightArrow ))
			controller.MoveRight();

		if( Input.GetKeyDown(KeyCode.Z))
			shot.Shoot();

		if( Input.GetKeyDown(KeyCode.LeftShift))
			Time.timeScale =  Time.timeScale == 0 ? 1 : 0;
	}
}
