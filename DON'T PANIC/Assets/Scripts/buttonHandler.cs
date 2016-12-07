using UnityEngine;
using System.Collections;

public class buttonHandler : MonoBehaviour {

    public GameObject thisButton;
    public GameObject laserToStart;
    public bool isPushed;
    private LaserTip_LaserScript laserscript;

    // Use this for initialization
    void Start () {
        isPushed = false;
        laserscript = laserToStart.GetComponent<LaserTip_LaserScript>();
    }

    IEnumerator laserTimer(float time)
    {
        laserscript.isOn = true;
        yield return new WaitForSeconds(3.0f);
        laserscript.isOn = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (isPushed == true)
        {
            if (laserscript.isOn == false)
            {
                StartCoroutine(laserTimer(3f));
                Debug.Log("Button Pushed! Firing Laser!");
                isPushed = false;
            }
            else
            {
                Debug.Log("It's already on!");
                isPushed = false;
            }
        }


    }
}
