
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserTip_LaserScript : MonoBehaviour
{
	public delegate void LaserCollidesStatue();
	public static event LaserCollidesStatue OnLaserCollidesStatue;

	public float 	LaserWidth;
	public int 		LaserLength;		
	public int 		MaxReflections;		
	public Material RedMat;

	private 		LineRenderer 	LaserLR;		//Laser Line Renderer
	private 		Light			FlareLight;		//Flare Light

	// Use this for initialization
	void Start()
	{
		LaserLR= 			gameObject.GetComponent<LineRenderer>();
		LaserLR.enabled=	false;

		FlareLight= 		gameObject.GetComponent<Light>();
		FlareLight.enabled=	false;

		LaserWidth= 		0.05f;
		LaserLength=		10000;	//random but greater than 500 as diagonal line render would be approx 500 
		MaxReflections=		300;	//3 per reflection

	}

	// Update is called once per frame
	void Update()
	{
		if( Input.GetButtonDown("Fire1") )//fix- Replace the controller button name
		{
			StopCoroutine ( FireLaser() );
			StartCoroutine( FireLaser() );
		}
	}

	IEnumerator FireLaser()
	{
		int 	ReflectionCount= 	1; 		//Laser Reflection count, just a precaution to avoid looping forever
		int 	NumOfVertexes= 		1; 		
		bool 	LoopBool= 			true; 	

		Vector3 LaserDirection = 	transform.forward; //direction of the next laser
		Vector3 PrevLaserPosition = transform.position; //origin of the next laser

		LaserLR.enabled= 			true;
		FlareLight.enabled= 		true;

		LaserLR.SetVertexCount( NumOfVertexes );
		LaserLR.SetPosition(0, transform.position);

		RaycastHit LaserRayHit;
		while(Input.GetButton("Fire1"))//fix- Replace the controller button name
		{	
			while (LoopBool)
			{
				if (Physics.Raycast( PrevLaserPosition, LaserDirection, out LaserRayHit, LaserLength ))
				{
					NumOfVertexes += 2;
					LaserLR.SetVertexCount(NumOfVertexes);


					LaserLR.SetPosition(NumOfVertexes - 2, Vector3.MoveTowards(LaserRayHit.point, PrevLaserPosition, 0.05f));
					LaserLR.SetPosition(NumOfVertexes - 1, LaserRayHit.point);
					LaserLR.SetWidth(LaserWidth, LaserWidth);

					PrevLaserPosition = LaserRayHit.point;
					LaserDirection = 	Vector3.Reflect(LaserDirection, LaserRayHit.normal);

					if ((LaserRayHit.collider.tag != "MirrorCollider") && (LaserRayHit.collider.tag != "SoldierCollider"))
					{
						LoopBool = false;
					}
					else if (LaserRayHit.collider.tag == "SoldierCollider") 
					{
						LoopBool = false;
						if (OnLaserCollidesStatue != null) 
						{	
							OnLaserCollidesStatue ();
						}
						
					}	
					else 
					{
						//LaserLR.material = RedMat;
					}
					ReflectionCount++;

				}
				else
				{
					LoopBool = false;
				}

				if (ReflectionCount > MaxReflections) 
				{	
					LoopBool = false;
				}
			}
			yield return new WaitForEndOfFrame();
		}
		LaserLR.enabled= 			false;
		FlareLight.enabled= 		false;

	}
}
