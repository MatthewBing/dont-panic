using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TriggeredAudio : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayDelayed(2);
    }
}