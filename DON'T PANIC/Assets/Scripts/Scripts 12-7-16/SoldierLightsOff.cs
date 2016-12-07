using UnityEngine;
using System.Collections;

public class SoldierLightsOff : MonoBehaviour {

	private	Light FlareLight;		//Flare Light

	void Start(){
		FlareLight= 			gameObject.GetComponent<Light>();	
		FlareLight.enabled = 	false;
	}
	void OnEnable()
	{
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff += SoldierFlaresOff;
	}

	void OnDisable()
	{
		FlareLight.enabled = 	false;
		LaserTip_LaserScript.OnLaserCollidesStatueLightsOff -= SoldierFlaresOff;
	}

	void SoldierFlaresOff()
	{
		FlareLight.enabled = 	false;
	}

}
