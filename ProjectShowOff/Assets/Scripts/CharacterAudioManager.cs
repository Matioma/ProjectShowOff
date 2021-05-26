using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;


    [SerializeField]
    CharacterAudioSet characterAudioSet;

    public void PlayAudio(AudioClip audio) {
        audioSource?.PlayOneShot(audio);
    }


    public void PlayStep() {
        AudioClip audio = characterAudioSet.GetStepSound();
        if (audio != null) audioSource.PlayOneShot(audio);
    }

    public void SpecialSkillSound()
    {
        AudioClip audio = characterAudioSet.GetSpecialSound();
        if (audio != null) audioSource.PlayOneShot(audio);
    }

}
