using UnityEngine;
using System.Collections;

public class KnightLightsOn : MonoBehaviour {

	public	Light FlareLight;		//Flare Light

	void Start(){
			
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
