using UnityEngine;
using System.Collections;

public class buttonHandler : MonoBehaviour {

    public GameObject thisButton;
    public GameObject laserToStart;
    public bool isPushed;
    private LaserTip_LaserScript laserscript;
    public int laserTime;

    // Use this for initialization
    void Start () {
        isPushed = false;
        laserscript = laserToStart.GetComponent<LaserTip_LaserScript>();
    }

    IEnumerator laserTimer(float time)
    {
        laserscript.isOn = true;
        yield return new WaitForSeconds(laserTime);
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
