
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserTip_LaserScript : MonoBehaviour
{
    public bool isOn;
    public bool RedLensHit;

    public float LaserWidth;
    public int LaserLength;
 
    public Material StartingMat;
    public Material RedMat;
    public Material GreenMat;
    public Material BlueMat;

    private Light FlareLight;

    private LineRenderer beam;
    public Transform laserHit;

    public GameObject lazerbeam;

    void SetLineRendererProperties(LineRenderer LR, Material LRMaterial)
    {
        LR.SetWidth(LaserWidth, LaserWidth);
        LR.material = LRMaterial;
    }

    IEnumerator RedLens(float time, Transform spot)
    {
        RedLensHit = true;
        Instantiate(lazerbeam, spot.position, Quaternion.identity);
        yield return new WaitForSeconds(time);
        RedLensHit = false;
    }


    // Use this for initialization
    void Start()
    {
        
        isOn = false;

        beam = GetComponent<LineRenderer>();
        beam.useWorldSpace = true;
        beam.enabled = true;

        FlareLight = gameObject.GetComponent<Light>();
        FlareLight.enabled = false;

        LaserWidth = 0.05f;
        LaserLength = 100000;   //random but greater than 500 as diagonal line render would be approx 500 

    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {

            Ray ray = new Ray(transform.position, transform.forward);
            beam.SetPosition(0, ray.origin);
            //beam.SetPosition(1, ray.GetPoint(100));
            Debug.DrawRay(ray.origin, ray.GetPoint(100), Color.green);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                beam.SetPosition(1, hit.point);
                GameObject objectHit = hit.transform.gameObject;
                string tagString = objectHit.tag;

                if (tagString == ("RedLensCollider"))
                {
                    print("Red Lens Hit");
                    if (!RedLensHit)
                    {
                        RedLens(3f, objectHit.transform);
                        //SetLineRendererProperties(beam, RedMat);
                    }
                    
                        
                 }

                else if (tagString.Equals("BlueLensCollider"))
                {

                }

                else if (tagString.Equals("GreenLensCollider"))
                {

                }

                else if (tagString.Equals("RedLensCollider"))
                {

                }

                else if (tagString.Equals("MirrorCollider"))
                {

                }

                else if (tagString.Equals("SplitCollider"))
                {

                }

            }

            //Otherwise, make the beam go farrrrrrr
            else
            {
                beam.SetPosition(1, ray.GetPoint(100));
            }
        }
        //end of if(isOn)
         

       
            


    }
}
