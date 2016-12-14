
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserTip_LaserScript : MonoBehaviour
{
    public delegate void LaserCollidesStatueLightsOn();
    public static event LaserCollidesStatueLightsOn OnLaserCollidesStatueLightsOn;


    public delegate void LaserCollidesStatueLightsOff();
    public static event LaserCollidesStatueLightsOff OnLaserCollidesStatueLightsOff;
    public bool isOn;

    public float LaserWidth;
    public int LaserLength;
    public int MaxReflections;

	public GameObject Knight; 
	public GameObject Soldier;
	public GameObject Woman;

	public GameObject statueWithScript;

	private whenHit scriptOnStatue;

    public Material RedMat;
    public Material GreenMat;
    public Material BlueMat;
    public Material ReflectorMaterial;
    public Material AdditionalMaterial1;
    public Material AdditionalMaterial2;


	private Dictionary<string, Material> 	LensColliderMaterialMap = 	new Dictionary<string, Material>();
	//private Dictionary<string, GameObject> 	StatueColliderNamesMap = 	new Dictionary<string,  GameObject>();
	private Dictionary<string, Material> 	StatueColliderNamesMap = 	new Dictionary<string,  Material>();
    private Light FlareLight;       //Flare Light


    void SetLineRendererProperties(LineRenderer LR, Material LRMaterial)
    {
        //print(LRMaterial);
        LR.SetWidth(LaserWidth, LaserWidth);
        LR.material = LRMaterial;
    }

    // Use this for initialization
    void Start()
    {

        FlareLight = gameObject.GetComponent<Light>();
        FlareLight.enabled = false;

        LaserWidth = 0.02f;
        LaserLength = 100000;   //random but greater than 500 as diagonal line render would be approx 500 
        MaxReflections = 300;   //3 per reflection

        
		//Lens Collider and the corresponding Material map creation
        LensColliderMaterialMap.Add("RedLensCollider", RedMat);
        LensColliderMaterialMap.Add("GreenLensCollider", GreenMat);
        LensColliderMaterialMap.Add("BlueLensCollider", BlueMat);

        //Populate the StatueColliderNames list
		StatueColliderNamesMap.Add("SoldierCollider", GreenMat);
		StatueColliderNamesMap.Add("KnightCollider", RedMat);
		StatueColliderNamesMap.Add("WomanCollider", BlueMat);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire1") )//fix- Replace the controller button name
        {
            StopCoroutine(FireLaser());
            StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {

        int ReflectionCount = 1;        //Laser Reflection count, just a precaution to avoid looping forever
        float SplitterzOffset = 1.0f;

        List<int> NumOfDiffMaterials = new List<int>();
        List<List<LineRenderer>> LaserLR = new List<List<LineRenderer>>();
        //Laser Line Renderer. Dimension 1- All beams. Dimension 2- All new LR because of passing through the lens, different materials 
        List<List<GameObject>> GameObj = new List<List<GameObject>>();      //Laser Line Renderer
                                                                            //1 LR can be only attached to 1 Game Object
        List<int> NumOfVertexes = new List<int>();
        //Number of vertexes making the segments in a line renderer. Its 1D as on passing through lens I just reset it to the initial value and reuse
        List<Vector3> LaserDirection = new List<Vector3>(); //direction of the next laser. 1D as its reused on passing through the lens
        List<Vector3> PrevLaserPosition = new List<Vector3>(); //Previous position of the laser. 1D as its reused
        List<Vector3> PrevDirection = new List<Vector3>(); //temporary variable, currently unused
        List<bool> LoopBool = new List<bool>(); //1D as its for every BEAM, when a BEAM after ALL reflections hits an unknown tag it stops

        int LRNumber = 0;

        NumOfDiffMaterials.Add(0);

        GameObj.Add(new List<GameObject>());
        GameObj[LRNumber].Add(new GameObject());

        LaserLR.Add(new List<LineRenderer>());
        LaserLR[LRNumber].Add(GameObj[LRNumber][NumOfDiffMaterials[LRNumber]].AddComponent<LineRenderer>());


        NumOfVertexes.Add(1);

        LaserDirection.Add(transform.forward);

        PrevLaserPosition.Add(transform.position);

        LoopBool.Add(true);

        //Setup the First LR
        LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].enabled = true;
        FlareLight.enabled = true;
        LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetVertexCount(NumOfVertexes[LRNumber]);
        LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetPosition(0, transform.position);
        SetLineRendererProperties(LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]], ReflectorMaterial);//fix

		while (Input.GetButtonDown("Fire1") )//fix- Replace the controller button nameisOn
        {
            //Every Loop is 1 Beam, if splitter encountered a new beam is initialized which is serviced by this same for loop later.
            //CurrentLR is updated whenever a color lens is encountered and hence, reused
            for (int LRCounter = 0; LRCounter <= LRNumber; LRCounter++)
            {
				//print ("For Counter " + LRCounter);
                RaycastHit LaserRayHit;
				string ColliderTag;
				LaserLR[LRCounter][NumOfDiffMaterials[LRNumber]].enabled = true;
                LineRenderer CurrentLR = LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]]; //	
				LineRenderer TempLR;
				PrevDirection.Add(LaserDirection[LRCounter]);

                while (LoopBool[LRCounter])
                {
					//print ("PrevPos: " + PrevLaserPosition[LRCounter]);
                    if (Physics.Raycast(PrevLaserPosition[LRCounter], LaserDirection[LRCounter], out LaserRayHit, LaserLength))
                    {
						
                        NumOfVertexes[LRCounter] += 1;

                        ColliderTag = LaserRayHit.collider.tag;

						CurrentLR.SetVertexCount(NumOfVertexes[LRCounter]);

						//print (CurrentLR.material + " " + LaserRayHit.point + " " + PrevLaserPosition[LRCounter] + "VC: " + NumOfVertexes[LRCounter]);
                        CurrentLR.SetPosition(NumOfVertexes[LRCounter] - 1, Vector3.MoveTowards(LaserRayHit.point, PrevLaserPosition[LRCounter], 0.05f));

                        PrevLaserPosition[LRCounter] = LaserRayHit.point;
						PrevDirection [LRCounter] = LaserDirection [LRCounter];
						//print (" Start Point: " + PrevLaserPosition[LRCounter]);
						//print (" LRCounter: " + LRCounter + " Material Change Number: " + NumOfDiffMaterials[LRCounter]);
						//print ("Laser Direction Before Reflect: " + LaserDirection[LRCounter]);
						LaserDirection[LRCounter] = Vector3.Reflect(LaserDirection[LRCounter], LaserRayHit.normal);
						//print ("Laser Direction After Reflect: " + LaserDirection [LRCounter]);
						//print ("Hit Surface Normal: " + LaserRayHit.normal);

						Vector3 SwapVec= new Vector3(0,0,0);
						//print ("PrevPos Before Hack: " + PrevLaserPosition[LRCounter]);

						if (!LensColliderMaterialMap.ContainsKey (ColliderTag)) {
							if (LaserDirection [LRCounter].x >= 0.1f)
								SwapVec.x = PrevLaserPosition [LRCounter].x + .1f;
							else if (LaserDirection [LRCounter].x < 0)
								SwapVec.x = PrevLaserPosition [LRCounter].x - .1f;
							else
								SwapVec.x = PrevLaserPosition [LRCounter].x;

							if (LaserDirection [LRCounter].y >= 0.1f)
								SwapVec.y = PrevLaserPosition [LRCounter].y + .1f;
							else if (LaserDirection [LRCounter].y < 0)
								SwapVec.y = PrevLaserPosition [LRCounter].y - .1f;
							else
								SwapVec.y = PrevLaserPosition [LRCounter].y;
						
							if (LaserDirection [LRCounter].z >= 0.1f)
								SwapVec.z = PrevLaserPosition [LRCounter].z + .1f;
							else if (LaserDirection [LRCounter].z < 0)
								SwapVec.z = PrevLaserPosition [LRCounter].z - .1f;
							else
								SwapVec.z = PrevLaserPosition [LRCounter].z;
						

						PrevLaserPosition[LRCounter]= SwapVec;
						}

						//print ("PrevPos After Hack: " + PrevLaserPosition[LRCounter]);
						//print("Hit collider Name: " + ColliderTag);

						if ((ColliderTag != "MirrorCollider") && (ColliderTag != "SplitCollider") && 
							!StatueColliderNamesMap.ContainsKey(ColliderTag) && !LensColliderMaterialMap.ContainsKey(ColliderTag))
						{
							//print ("In No collision");
                            LoopBool[LRCounter] = false;
							print (LRCounter + " " + LaserRayHit.point);
                        }
						else if (ColliderTag == "SplitCollider")
                        {

							//Beam 1
							//print ("In SplitCollider");
                            LRNumber++;

                            NumOfDiffMaterials.Add(0);

                            GameObj.Add(new List<GameObject>());
                            GameObj[LRNumber].Add(new GameObject());

                            LaserLR.Add(new List<LineRenderer>());
                            LaserLR[LRNumber].Add(GameObj[LRNumber][NumOfDiffMaterials[LRNumber]].AddComponent<LineRenderer>());


                            NumOfVertexes.Add(1);
                            LaserDirection.Add(-LaserDirection[LRCounter]);

							PrevLaserPosition.Add(PrevLaserPosition[LRCounter]);


							//print ("PrevPos Original: " + PrevLaserPosition[LRCounter]);
							//print ("PrevPos For the New Split beam: " + PrevLaserPosition[LRNumber]);
							//print ("Laser direction Original: " + LaserDirection[LRCounter]);
							//print ("Laser direction Original for the New Split beam: " + LaserDirection[LRNumber]);
                            LoopBool.Add(true);

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].enabled = true;
                            SetLineRendererProperties(LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]], CurrentLR.material);


                            TempLR = CurrentLR;
                            CurrentLR = LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]];

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetVertexCount(NumOfVertexes[LRNumber]);

                             if (Physics.Raycast(PrevLaserPosition[LRNumber], LaserDirection[LRNumber], out LaserRayHit, LaserLength))
                            {
								PrevLaserPosition[LRNumber] = LaserRayHit.point;
								//print ("PrevPos For the New Split beam after Collision: " + PrevLaserPosition[LRNumber]);

								if(LaserDirection [LRNumber].x >= 0.1f)
									SwapVec.x = PrevLaserPosition[LRNumber].x + .1f;
								else if(LaserDirection [LRNumber].x < 0)
									SwapVec.x = PrevLaserPosition[LRNumber].x - .1f;
								else
									SwapVec.x = PrevLaserPosition[LRNumber].x;	
								
								if(LaserDirection [LRNumber].y >= 0.1f)								
									SwapVec.y = PrevLaserPosition[LRNumber].y + .1f;
								else if(LaserDirection [LRNumber].y < 0)				
									SwapVec.y = PrevLaserPosition[LRNumber].y - .1f;
								else	
									SwapVec.y = PrevLaserPosition[LRNumber].y;
								
								if(LaserDirection [LRNumber].z >= 0.1f)
									SwapVec.z = PrevLaserPosition[LRNumber].z + .1f;
								else if(LaserDirection [LRNumber].z < 0)
									SwapVec.z = PrevLaserPosition[LRNumber].z - .1f;
								else	
									SwapVec.z = PrevLaserPosition[LRNumber].z;


								PrevLaserPosition[LRNumber] = SwapVec;
								//print ("PrevPos For the New Split beam after Hack: " + PrevLaserPosition[LRNumber]);
                            }
								
                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetPosition(0, PrevLaserPosition[LRNumber]);
                            CurrentLR = TempLR;




                        }
						else if (StatueColliderNamesMap.ContainsKey(ColliderTag))
						{
							print ( (LaserRayHit.collider.GetComponentInParent<whenHit>()).matneeded.ToString());
							print ("LR: " + GreenMat.ToString());
							statueWithScript = GameObject.FindGameObjectWithTag(ColliderTag);

							//if (CurrentLR.material.Equals(statueWithScript.GetComponent<Material>())) {	
							if(StatueColliderNamesMap[ColliderTag].ToString() == (LaserRayHit.collider.GetComponentInParent<whenHit>()).matneeded.ToString()){
								//StatueColliderNamesMap [ColliderTag])
								print ("OH WELL");

								scriptOnStatue = statueWithScript.GetComponent<whenHit> ();
								scriptOnStatue.hitByCorrectLaser();

							}//MIGHT be a problem, ALL STATUES MIGHT light up at the same time, can be easily fixed in the morning. 5mins
                            /*LoopBool[LRCounter] = false;
                            if (OnLaserCollidesStatueLightsOn != null)
                            {
                                OnLaserCollidesStatueLightsOn();
                            }*/
							LoopBool[LRCounter] = false;

						} 
                        else if (LensColliderMaterialMap.ContainsKey(ColliderTag))
                        {

							//print ("In LensCollider");
							NumOfDiffMaterials[LRCounter]=NumOfDiffMaterials[LRCounter] + 1;
                            GameObj[LRCounter].Add(new GameObject());
                            LaserLR[LRCounter].Add(GameObj[LRCounter][NumOfDiffMaterials[LRCounter]].AddComponent<LineRenderer>());
                            //print(ColliderTag);
                            SetLineRendererProperties(LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]], LensColliderMaterialMap[ColliderTag]);

                            NumOfVertexes[LRCounter] = 1;

							//print ("LC: " + NumOfDiffMaterials[LRCounter] + "VC: " +  NumOfVertexes[LRCounter]);
							LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]].enabled = true;
							LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]].SetVertexCount(NumOfVertexes[LRCounter]);
							//print ("Original Laser Direction: " + LaserDirection [LRCounter]);
							LaserDirection [LRCounter] = PrevDirection[LRCounter];
							//print ("Previous and after lens Laser Direction: " + LaserDirection [LRCounter]);
							//if (Physics.Raycast (PrevLaserPosition [LRCounter], LaserDirection [LRCounter], out LaserRayHit, LaserLength)) {
								//PrevLaserPosition [LRCounter] = LaserRayHit.point;
								//print ("PrevPos Lens Collider: " + PrevLaserPosition [LRCounter]);

								if (LaserDirection [LRCounter].x >= 0.1f) 
									SwapVec.x = PrevLaserPosition [LRCounter].x + 0.1f;
								else if (LaserDirection [LRCounter].x < 0) 
									SwapVec.x = PrevLaserPosition [LRCounter].x - 0.1f;
								else
									SwapVec.x = PrevLaserPosition [LRCounter].x;
								

								if (LaserDirection [LRCounter].y >= 0.1f)
									SwapVec.y = PrevLaserPosition [LRCounter].y + 0.1f;
								else if (LaserDirection [LRCounter].y < 0)
									SwapVec.y = PrevLaserPosition [LRCounter].y - 0.1f;
								else
									SwapVec.y = PrevLaserPosition [LRCounter].y;
							
								if (LaserDirection [LRCounter].z >= 0.1f)
									SwapVec.z = PrevLaserPosition [LRCounter].z + 0.1f;
								else if (LaserDirection [LRCounter].z < 0)
									SwapVec.z = PrevLaserPosition [LRCounter].z - 0.1f;
								else
									SwapVec.z = PrevLaserPosition [LRCounter].z;

							//print (SwapVec.x);

								PrevLaserPosition [LRCounter] = SwapVec;
								//print ("PrevPos Lens Collider after Hack: " + PrevLaserPosition [LRCounter]);
								//LaserDirection [LRCounter] = LaserRayHit.normal;

							//}
							//print("GREENWALA!: " + PrevLaserPosition[LRNumber] + "vs: " +  PrevLaserPosition[LRCounter]);
							LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]].SetPosition(0, PrevLaserPosition[LRCounter]);

							CurrentLR = LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]];
                            //LaserDirection[LRCounter] = -LaserDirection[LRCounter];

                        }
                        else
                        {
                            //LaserLR.material = RedMat;
                        }
                        ReflectionCount++;

                    }
                    else
					{
						//print (LRCounter + " Hit Point " + LaserRayHit.point);
                        LoopBool[LRCounter] = false;
                    }
					/*
                    if (ReflectionCount > MaxReflections)
                    {
                        LoopBool[LRCounter] = false;
                    }*/
                }
                //print ("LASER exit " + LRCounter );
            }
            yield return new WaitForEndOfFrame();
        }

        //Button unpressed- turn off all laser beams
        for (int LRCounter = 0; LRCounter <= LRNumber; LRCounter++)
            for (int NumOfDiffMat = 0; NumOfDiffMat < LaserLR[LRCounter].Count; NumOfDiffMat++)
                LaserLR[LRCounter][NumOfDiffMat].enabled = true;

        //FlareLight.enabled = false;
        //OnLaserCollidesStatueLightsOff();

    }
}
