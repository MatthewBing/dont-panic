using UnityEngine;
using System.Collections;

public class buttonHandler : MonoBehaviour {

    public GameObject thisButton;
    public GameObject laserToStart;
    public bool isPushed;
    private CannonBehavior laserscript;
    public int laserTime;

    // Use this for initialization
    void Start () {
        isPushed = false;
        laserscript = laserToStart.GetComponent<CannonBehavior>();
    }

    IEnumerator fireLaser(float time)
    {
        laserscript.isPushed = true;
        yield return new WaitForSeconds(laserTime);
        laserscript.isPushed = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (isPushed == true)
        {
            if (laserscript.isPushed == false)
            {
                StartCoroutine(fireLaser(3f));
                isPushed = false;
            }
            else
            {
                Debug.Log("Wait a sec, jeez!");
                isPushed = false;
            }
        }


    }
}
