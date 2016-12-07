using UnityEngine;
using System.Collections;

public class DontPanicAudio : MonoBehaviour
{

    public AudioSource behindtheears;

    IEnumerator playClip(float time)
    {
        yield return new WaitForSeconds(time);
        behindtheears.Play();

    }

    void Start()
    {

        StartCoroutine(playClip(17f));
    }

    void Update()
    {

    }
}