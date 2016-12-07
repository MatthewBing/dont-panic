using UnityEngine;
using System.Collections;

public class SoldierLightsOff : MonoBehaviour {

	public	Light FlareLight;		//Flare Light

	void Start(){
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
