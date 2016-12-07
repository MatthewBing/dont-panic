using UnityEngine;
using System.Collections;

public class KnightLightsOff : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff += KnightFlaresOff;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff -= KnightFlaresOff;
	}

	void KnightFlaresOff()
	{
		print ("In Knights Script Off");
		FlareLight.enabled = 	false;
	}

}
