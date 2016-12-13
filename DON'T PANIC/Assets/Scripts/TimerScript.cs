using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour 
{

	private float 	CountdownTimer;
	public int	timerLength;
	public int	delay;
    public Text 	TimerT;
    string MinT;
    string SecT;


    // Use this for initialization
    void Start () 
	{
        TimerT = GetComponent<Text> ();
        StartCoroutine(WaitBeforeStarting(delay));
        int timeRem = timerLength + delay;
        MinT = (timeRem / 60).ToString();
        SecT = (timeRem % 60).ToString();
        
        
    }
	
	// Update is called once per frame
	void Update () 
	{


    }
    IEnumerator WaitBeforeStarting(int time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(CountDownTimer(300));
    }

	IEnumerator CountDownTimer(int timeRem)
	{

            timeRem = ((int)(timerLength - Time.realtimeSinceStartup));

			MinT =	(timeRem / 60).ToString ();
            SecT = (timeRem % 60).ToString();


            if (timeRem <= 0) 
			{
				//fix-game over 
			} 
			else if (timeRem <= 30) 
			{
			    TimerT.color =	Color.red;
                TimerT.text = MinT + ":" + SecT;
			} 
			else 
			{
                TimerT.text = MinT + ":" + SecT;
			}
                //+ ":" + SecT);
		
        yield return new WaitForSecondsRealtime(.5f);
        StartCoroutine(CountDownTimer(timeRem));
    }
}
