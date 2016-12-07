using UnityEngine;
using System.Collections;

public class KnightLightsOn : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn += KnightFlares;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOn -= KnightFlares;
	}

	void KnightFlares()
	{
		print ("In Knights Script On");
		FlareLight.enabled = 	true;
	}

}
