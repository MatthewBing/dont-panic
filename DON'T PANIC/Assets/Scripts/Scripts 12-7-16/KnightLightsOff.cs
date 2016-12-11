using UnityEngine;
using System.Collections;

public class KnightLightsOff : MonoBehaviour {

	public Light FlareLight;		//Flare Light

	void Start(){
		
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
		
		FlareLight.enabled = 	false;
	}

}
