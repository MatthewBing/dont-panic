using UnityEngine;
using System.Collections;

public class WomanLightsOff : MonoBehaviour {

	public	Light FlareLight;		//Flare Light

	void Start(){	
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
		
		FlareLight.enabled = 	false;
	}

}
