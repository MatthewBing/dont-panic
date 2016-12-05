using UnityEngine;
using System.Collections;

public class buttonHandler : MonoBehaviour {

    public GameObject thisButton;
    public GameObject ovenFireToStart;
    public GameObject laserToStart;
    public bool isPushed;

    // Use this for initialization
    void Start () {
        isPushed = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            isPushed = true;
            
        }
        
    }
}
