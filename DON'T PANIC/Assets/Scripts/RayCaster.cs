using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {

    private buttonHandler buttonscript;
    RaycastHit hit;
	public Texture theTexture;

	// Use this for initialization
	void Start () {
        buttonscript = GameObject.FindGameObjectWithTag("LaserButton").GetComponent<buttonHandler>();
    }

	void onGUI()
	{
		GUI.DrawTexture (new Rect (Screen.width / 2, Screen.height / 2, 55f, 55f), theTexture);
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
			if (Physics.Raycast (transform.position, transform.forward, out hit, 2.0F)) {
				if (hit.transform.tag == "LaserButton") {
					buttonscript.isPushed = true;
				
				} else if (hit.transform.tag == "Pickable") {
					
				}
			}
        }
    }
}
