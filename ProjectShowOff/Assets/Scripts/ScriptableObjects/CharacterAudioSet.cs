using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSet", menuName = "AudioManagers/CharacterSet", order = 1)]
public class CharacterAudioSet : ScriptableObject
{
    [SerializeField]
    List<Pair> AudioClips;


    public AudioClip GetAudio() {
        return AudioClips[0].clips[0];
    }

    public AudioClip GetRandomStepSound()
    {
        Pair pairSteps = getPairOfType(EAudioClipType.Movement);
        if (pairSteps != null) { 
            return pairSteps.getRandomSound();
        }
        return null;
    }


    public AudioClip GetStepSound() {
        Pair pairSteps = getPairOfType(EAudioClipType.Movement);
        if (pairSteps != null) {
            return pairSteps.getNextClip();
        }
        return null;
    }

    public AudioClip GetSpecialAbilitySound() {
        Pair pairSteps = getPairOfType(EAudioClipType.SpecialAbility);
        if (pairSteps != null)
        {
            return pairSteps.getRandomSound();
        }
        return null;
    }


    public AudioClip getSoundOfType(EAudioClipType soundType) {
        Pair pairSteps = getPairOfType(soundType);
        if (pairSteps != null)
        {
            return pairSteps.getRandomSound();
        }
        return null;
    }

    public Pair getPairOfType(EAudioClipType animation) {
        foreach (var pair in AudioClips)
        {
            if (pair.aniomationType == animation)
            {
                return pair;

            }
        }
        Debug.LogWarning("The Animation pair for step was not defined in the scripteable object instance");
        return null;
    }

}



public enum EAudioClipType{ 
    Movement,
    SpecialAbility,
    Landing
}


[System.Serializable]
public class Pair{
    [SerializeField]
    public EAudioClipType aniomationType;
    [SerializeField]
    public List<AudioClip> clips;

    int clipId = 0;

    public AudioClip getRandomSound() {
        if (clips.Count <= 0) {
            Debug.LogWarning("the list of clips in CharacterAudio set is empty");
            return null;
        }
        return clips[Random.Range(0, clips.Count)];
    }


    public AudioClip getNextClip() {
        if (clips.Count <= 0)
        {
            Debug.LogWarning($"the list of clips in CharacterAudio  of {aniomationType} set is empty");
            return null;
        }
        clipId = (clipId + 1) % clips.Count;
        return clips[clipId];
    }
}
