using UnityEngine;
using System.Collections;
using Cyberconian.Unity;
public class NewBehaviourScript : MonoBehaviour {


	void Update () {
	
	}

void SetDisplay (string displayValue)
    {
        GameObject.Find("7SDG").GetComponent<SevenSegmentDriver>().Data = displayValue;
    }
}
