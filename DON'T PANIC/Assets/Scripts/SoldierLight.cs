using UnityEngine;
using System.Collections;

public class SoldierLight : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		print ("In Enable");
		LaserTip_LaserScript.OnLaserCollidesStatue += SoldierFlares;
	}

	void OnDisable()
	{
		print ("In Disable");
		FlareLight.range= 0;
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatue -= SoldierFlares;
	}

	void SoldierFlares()
	{
		FlareLight.enabled = 	true;
	}

}
