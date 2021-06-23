using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    FMODUnity.StudioEventEmitter emitter;

    public void Play() {
        emitter.Play();
        Debug.Log("Play sound");
    }
}
