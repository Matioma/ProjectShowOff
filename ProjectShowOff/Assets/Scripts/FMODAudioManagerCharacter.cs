using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



enum AudioTypes{ 
    

}

[RequireComponent(typeof(CharachterModel))]
public class FMODAudioManagerCharacter : MonoBehaviour
{
    
    //[SerializeField]
    //[EventRef]
    //private string BackgroundMusicEvent = "";

    [SerializeField]
    StudioEventEmitter emitter;

    private FMOD.Studio.EventInstance backrgoundMusicInstance;
    void Start()
    {
      
    }

    

    public void StartBackground() {
        emitter.Play();
        //Debug.Log("Start music");
        //backrgoundMusicInstance.
        //backrgoundMusicInstance.start();
    }

    public void StopBackground() {
        emitter.Stop();
        //backrgoundMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            backrgoundMusicInstance.start();
            //instance.start();
        }
    }
}
