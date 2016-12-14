using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
    //particles: https://www.youtube.com/watch?v=IX-kgWfecn4
    //https://unity3d.com/learn/tutorials/topics/graphics/fun-lasers
    //http://answers.unity3d.com/questions/496989/laser-beam-texture.html

    private LineRenderer lineRenderer;
    public Transform laserHit;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

       // if (Input.GetKeyDown(KeyCode.Space))
       // {
         //   lineRenderer.enabled = !lineRenderer.enabled;

            if (lineRenderer.enabled)
            {

            Ray ray = new Ray(transform.position, transform.forward);
            lineRenderer.SetPosition(0, ray.origin);
            //lineRenderer.SetPosition(1, ray.GetPoint(100));
            Debug.DrawRay(ray.origin, ray.GetPoint(100), Color.green);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                lineRenderer.SetPosition(1, hit.point);
                GameObject objectHit = hit.transform.gameObject;
                //do somthing with this object (change color, etc.)
            }
            else
            {
                lineRenderer.SetPosition(1, ray.GetPoint(100));
            }


            //RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward);
            //laserHit.position = hit[0].point;
        }
        //}

    }
}
