using UnityEngine;
using System.Collections;

public class LaserTip_LaserScript : MonoBehaviour 
{
	LineRenderer LaserLine;
	Light FlareLight;

	// Use this for initialization
	void Start () 
	{
		LaserLine = gameObject.GetComponent<LineRenderer> ();
		LaserLine.enabled = false;

		FlareLight= gameObject.GetComponent<Light> ();
		FlareLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetButtonDown("Fire1") )
		{
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}
	}

	IEnumerator FireLaser()
	{
		LaserLine.enabled= true;
		FlareLight.enabled = true;

		while( Input.GetButton("Fire1") )
		{
			LaserLine.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, Time.time);	

			Ray LaserRay=	new Ray( transform.position, transform.forward);
			RaycastHit LaserRayHit;

			LaserLine.SetPosition(0, LaserRay.origin);

			if (Physics.Raycast (LaserRay, out LaserRayHit, 400)) 
			{
				LaserLine.SetPosition (1, LaserRayHit.point);
				if( LaserRayHit.rigidbody)
				{
					LaserRayHit.rigidbody.AddForceAtPosition (transform.forward * 1, LaserRayHit.point);
				}	
			} 
			else 
			{	
				LaserLine.SetPosition(1, LaserRay.GetPoint(40));
			}
			yield return null;
		}	

		LaserLine.enabled= false;
		FlareLight.enabled = false;
	}
}
