using UnityEngine;
using System.Collections;

public class TVChangeAfter15Seconds : MonoBehaviour
{

    public Material TVMat;
    public Material texture_framework;

    IEnumerator turnOffPhoto(float time)
    {
        yield return new WaitForSeconds(time);
        TVMat = texture_framework;

    }

    void Start()
    {
        TVMat = GetComponent<Material>();
        StartCoroutine(turnOffPhoto(15f));
    }

    void Update()
    {
        
    }
}