using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharachterModel))]
public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;


    [SerializeField]
    CharacterAudioSet characterAudioSet;

    CharachterModel character;

    private void Awake()
    {
        character = GetComponent<CharachterModel>();
        character.OnLanding.AddListener(PlayLandSound);
        character.onUseSkill.AddListener(PlaySkill);
    }


    void PlayLandSound()
    {
        AudioClip audio = characterAudioSet.getSoundOfType(EAudioClipType.Landing);
        if (audio != null) audioSource.PlayOneShot(audio);
    }
    



    public void PlayAudio(AudioClip audio) {
        audioSource?.PlayOneShot(audio);
    }


    public void PlayRandomStep() {
        AudioClip audio = characterAudioSet.GetRandomStepSound();
        if (audio != null) audioSource.PlayOneShot(audio);
    }


    public void PlaySkill() {
        AudioClip audio = characterAudioSet.GetSpecialAbilitySound();
        if (audio != null) audioSource.PlayOneShot(audio);
    }


    public void PlayNextOrderedStep() {
        AudioClip audio = characterAudioSet.GetStepSound();
        if (audio != null) audioSource.PlayOneShot(audio);
    }

}
