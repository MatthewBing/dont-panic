using UnityEngine;
using System.Collections;

public class WomanLightsOff : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff += WomanFlaresOff;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff -= WomanFlaresOff;
	}

	void WomanFlaresOff()
	{
		print ("In Womans Script Off");
		FlareLight.enabled = 	false;
	}

}
