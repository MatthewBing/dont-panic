using UnityEngine;
using System.Collections;

public class SoldierLightsOn : MonoBehaviour {

	public Light FlareLight;		//Flare Light

	void Start(){

		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn += SoldierFlares;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn -= SoldierFlares;
	}

	void SoldierFlares()
	{
		FlareLight.enabled = 	true;
	}

}
