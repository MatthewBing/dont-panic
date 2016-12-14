using UnityEngine;
using System.Collections;

public class whenHit : MonoBehaviour {

	public bool hitWithinLastThree;
	public bool colorOn;
	public Light dislight;
	public GameObject tv;
	private TimerScript timer;
	public int puzzlecount;
	public Material matneeded;

	// Use this for initialization
	void Start () {
		hitWithinLastThree = false;
		timer = tv.GetComponent<TimerScript>();
		puzzlecount = timer.puzzlecounter;
	}

	IEnumerator activeFor(float time)
	{
		hitWithinLastThree = true;
		yield return new WaitForSeconds (time);
		hitWithinLastThree = false;
	}

	IEnumerator correctColorActiveFor(float time)
	{
		colorOn = true;
		puzzlecount += 1;
		yield return new WaitForSeconds (time);
		colorOn = false;
		puzzlecount -= 1;

	}

	public void hitByCorrectLaser()
	{
		StartCoroutine(correctColorActiveFor(3f));

	}

	
	// Update is called once per frame
	void Update () 
	{
		if (colorOn) {
			dislight.intensity = 4;

		} else
			dislight.intensity = 0;
			
	}
}
