using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour 
{

	public 	float 	CountdownTimer;
	private float 	StartTime;
	private float	StopTime;
	public 	Text 	TimerT;


	// Use this for initialization
	void Start () 
	{
		
		StopTime =	300;
		TimerT = GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")) {//fix- Replace the controller button name

			StopCoroutine ( CountDownTimer() );
			StartCoroutine( CountDownTimer() );
		}

	
	}

	IEnumerator CountDownTimer()
	{
		string MinT;
		string SecT;
		StartTime= 	Time.time;

		while (Input.GetButton ("Fire1")) {//fix- Replace the controller button name

			float DeltaTime =	StopTime - (Time.time - StartTime);

			MinT =	((int)DeltaTime / 60).ToString ();		
			SecT =	((int)DeltaTime % 60).ToString ();

			if (DeltaTime <= 0) 
			{
				//fix-game over 
			} 
			else if (DeltaTime <= 30) 
			{
				TimerT.color =	Color.red;
				TimerT.text =	MinT + ":" + SecT;
			} 
			else 
			{
				TimerT.text =	MinT + ":" + SecT;
			}
			print (MinT + ":" + SecT);

			yield return new WaitForEndOfFrame();
		}
	}
}
