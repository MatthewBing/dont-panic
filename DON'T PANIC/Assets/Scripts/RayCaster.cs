using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {

    private buttonHandler buttonscript;
    RaycastHit hit;
    

	// Use this for initialization
	void Start () {
        buttonscript = GameObject.FindGameObjectWithTag("LaserButton").GetComponent<buttonHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0F))
            {
                if (hit.transform.tag == "LaserButton")
                {
                    buttonscript.isPushed = true;
                }
            }
        }
    }
}
