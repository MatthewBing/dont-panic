
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

    public Material RedMat;
    public Material GreenMat;
    public Material BlueMat;
    public Material ReflectorMaterial;
    public Material AdditionalMaterial1;
    public Material AdditionalMaterial2;


    private Dictionary<string, Material> LensColliderMaterialMap = new Dictionary<string, Material>();
    private List<string> StatueColliderNames = new List<string>();
    private Light FlareLight;       //Flare Light


    void SetLineRendererProperties(LineRenderer LR, Material LRMaterial)
    {
        print(LRMaterial);
        LR.SetWidth(LaserWidth, LaserWidth);
        LR.material = LRMaterial;
    }

    // Use this for initialization
    void Start()
    {

        FlareLight = gameObject.GetComponent<Light>();
        FlareLight.enabled = false;

        LaserWidth = 0.05f;
        LaserLength = 100000;   //random but greater than 500 as diagonal line render would be approx 500 
        MaxReflections = 300;   //3 per reflection

        //Lens Collider and the corresponding Material map creation
        LensColliderMaterialMap.Add("RedLensCollider", RedMat);
        LensColliderMaterialMap.Add("GreenLensCollider", GreenMat);
        LensColliderMaterialMap.Add("BlueLensCollider", BlueMat);

        //Populate the StatueColliderNames list

        StatueColliderNames.Add("SoldierCollider");
        StatueColliderNames.Add("KnightCollider");
        StatueColliderNames.Add("WomanCollider");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)//fix- Replace the controller button name
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

        while (isOn)//fix- Replace the controller button name
        {
            //Every Loop is 1 Beam, if splitter encountered a new beam is initialized which is serviced by this same for loop later.
            //CurrentLR is updated whenever a color lens is encountered and hence, reused
            for (int LRCounter = 0; LRCounter <= LRNumber; LRCounter++)
            {
                RaycastHit LaserRayHit;
                string ColliderTag;
                LineRenderer CurrentLR = LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]]; //	
                LineRenderer TempLR;

                while (LoopBool[LRCounter])
                {
                    if (Physics.Raycast(PrevLaserPosition[LRCounter], LaserDirection[LRCounter], out LaserRayHit, LaserLength))
                    {

                        NumOfVertexes[LRCounter] += 1;
                        ColliderTag = LaserRayHit.collider.tag;

                        CurrentLR.SetVertexCount(NumOfVertexes[LRCounter]);

                        CurrentLR.SetPosition(NumOfVertexes[LRCounter] - 1, Vector3.MoveTowards(LaserRayHit.point, PrevLaserPosition[LRCounter], 0.05f));

                        PrevLaserPosition[LRCounter] = LaserRayHit.point;
                        PrevDirection.Add(LaserDirection[LRCounter]);
                        LaserDirection[LRCounter] = Vector3.Reflect(LaserDirection[LRCounter], LaserRayHit.normal);

                        if ((ColliderTag != "MirrorCollider") && (ColliderTag != "SoldierCollider") && (ColliderTag != "SplitCollider") && !LensColliderMaterialMap.ContainsKey(ColliderTag))
                        {
                            LoopBool[LRCounter] = false;
                            //print (LRCounter + " " + ColliderTag);
                        }
                        else if (ColliderTag == "SplitCollider")
                        {

                            //Beam 1
                            LRNumber++;

                            NumOfDiffMaterials.Add(0);

                            GameObj.Add(new List<GameObject>());
                            GameObj[LRNumber].Add(new GameObject());

                            LaserLR.Add(new List<LineRenderer>());
                            LaserLR[LRNumber].Add(GameObj[LRNumber][NumOfDiffMaterials[LRNumber]].AddComponent<LineRenderer>());


                            NumOfVertexes.Add(1);
                            LaserDirection.Add(-LaserDirection[LRCounter]);

                            PrevLaserPosition.Add(LaserRayHit.point);
                            LoopBool.Add(true);

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].enabled = true;
                            SetLineRendererProperties(LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]], CurrentLR.material);


                            TempLR = CurrentLR;
                            CurrentLR = LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]];

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetVertexCount(NumOfVertexes[LRNumber]);

                            if (Physics.Raycast(PrevLaserPosition[LRNumber], LaserDirection[LRNumber], out LaserRayHit, LaserLength))
                            {
                                PrevLaserPosition[LRNumber] = LaserRayHit.point;
                            }

                            Vector3 SwapVec;
                            SwapVec.y = PrevLaserPosition[LRNumber].y;
                            SwapVec.x = PrevLaserPosition[LRNumber].x;
                            /*if(LaserDirection [LRNumber].z > 0)
								SwapVec.z = PrevLaserPosition [LRNumber].z - 0.2f;	
							else
								SwapVec.z = PrevLaserPosition [LRNumber].z + 0.2f;	*/
                            SwapVec.z = PrevLaserPosition[LRNumber].z + 0.2f;
                            PrevLaserPosition[LRNumber] = SwapVec;
                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetPosition(0, PrevLaserPosition[LRNumber]);
                            CurrentLR = TempLR;

                            //Beam2 
                            LRNumber++;
                            NumOfDiffMaterials.Add(0);

                            GameObj.Add(new List<GameObject>());
                            GameObj[LRNumber].Add(new GameObject());

                            LaserLR.Add(new List<LineRenderer>());
                            LaserLR[LRNumber].Add(GameObj[LRNumber][NumOfDiffMaterials[LRNumber]].AddComponent<LineRenderer>());


                            NumOfVertexes.Add(1);
                            LaserDirection.Add(PrevDirection[LRCounter]);

                            PrevLaserPosition.Add(LaserRayHit.point);
                            LoopBool.Add(true);

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].enabled = true;
                            SetLineRendererProperties(LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]], CurrentLR.material);


                            TempLR = CurrentLR;
                            CurrentLR = LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]];

                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetVertexCount(NumOfVertexes[LRNumber]);

                            if (Physics.Raycast(PrevLaserPosition[LRNumber], PrevDirection[LRCounter], out LaserRayHit, LaserLength))
                            {
                                PrevLaserPosition[LRNumber] = LaserRayHit.point;
                            }

                            SwapVec.y = PrevLaserPosition[LRNumber].y;
                            SwapVec.x = PrevLaserPosition[LRNumber].x - 0.4f;
                            SwapVec.z = PrevLaserPosition[LRNumber].z;
                            PrevLaserPosition[LRNumber] = SwapVec;
                            LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]].SetPosition(0, PrevLaserPosition[LRNumber]);
                            CurrentLR = TempLR;



                        }
                        else if (StatueColliderNames.Contains(ColliderTag))
                        {
                            //MIGHT be a problem, ALL STATUES MIGHT light up at the same time, can be easily fixed in the morning. 5mins
                            LoopBool[LRCounter] = false;
                            if (OnLaserCollidesStatueLightsOn != null)
                            {
                                OnLaserCollidesStatueLightsOn();
                            }

                        }
                        else if (LensColliderMaterialMap.ContainsKey(ColliderTag))
                        {

                            NumOfDiffMaterials[LRCounter]++;
                            GameObj[LRCounter].Add(new GameObject());
                            LaserLR[LRCounter].Add(GameObj[LRCounter][NumOfDiffMaterials[LRCounter]].AddComponent<LineRenderer>());
                            print(ColliderTag);
                            SetLineRendererProperties(LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]], LensColliderMaterialMap[ColliderTag]);

                            NumOfVertexes[LRCounter] = 1;
                            LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]].SetVertexCount(NumOfVertexes[LRCounter]);
                            LaserLR[LRCounter][NumOfDiffMaterials[LRCounter]].SetPosition(0, PrevLaserPosition[LRNumber]);

                            CurrentLR = LaserLR[LRNumber][NumOfDiffMaterials[LRNumber]];
                            LaserDirection[LRCounter] = -LaserDirection[LRCounter];

                        }
                        else
                        {
                            //LaserLR.material = RedMat;
                        }
                        ReflectionCount++;

                    }
                    else
                    {
                        LoopBool[LRCounter] = false;
                    }

                    if (ReflectionCount > MaxReflections)
                    {
                        LoopBool[LRCounter] = false;
                    }
                }
                //print ("LASER exit " + LRCounter );
            }
            yield return new WaitForEndOfFrame();
        }

        //Button unpressed- turn off all laser beams
        for (int LRCounter = 0; LRCounter <= LRNumber; LRCounter++)
            for (int NumOfDiffMat = 0; NumOfDiffMat < LaserLR[LRCounter].Count; NumOfDiffMat++)
                LaserLR[LRCounter][NumOfDiffMat].enabled = false;

        FlareLight.enabled = false;
        OnLaserCollidesStatueLightsOff();

    }
}
