using UnityEngine;
using System.Collections;

public class slowIntensityIncrease : MonoBehaviour {

    new public Light light = null;
    private float EndTime = 5;
    private float StartTime = 0;

    IEnumerator slowLightTurnon(float time)
    {
        yield return new WaitForSeconds(time);
        light.intensity = 0;
        light.enabled = true;
    }

    void Start () {
        light = GetComponent<Light>();
        light.enabled = false;
        StartCoroutine(slowLightTurnon(1f));
    }
	
	void Update () {
        if (light && Time.time < 10)
            light.intensity = Time.time/5;
    }
}
