using UnityEngine;
using System.Collections;

public class WomanLightsOn : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn += WomanFlares;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn -= WomanFlares;
	}

	void WomanFlares()
	{
		print ("In Womans Script On");
		FlareLight.enabled = 	true;
	}

}
