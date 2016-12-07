using UnityEngine;
using System.Collections;

public class WomanLightsOn : MonoBehaviour {

	public	Light FlareLight;		//Flare Light

	void Start(){
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
