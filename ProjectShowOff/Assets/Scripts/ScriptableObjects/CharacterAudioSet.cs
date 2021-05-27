using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSet", menuName = "AudioManagers/CharacterSet", order = 1)]
public class CharacterAudioSet : ScriptableObject
{
    [SerializeField]
    List<Pair> animations;


    public AudioClip GetAudio() {
        return animations[0].clips[0];
    }

    public AudioClip GetStepSound()
    {
        Pair pairSteps = getPairOfType(EAnimation.Movement);
        if (pairSteps != null) { 
            return pairSteps.getRandomSound();
        }
        return null;
    }

    public AudioClip GetSpecialAbilitySound() {
        Pair pairSteps = getPairOfType(EAnimation.SpecialAbility);
        if (pairSteps != null)
        {
            return pairSteps.getRandomSound();
        }
        return null;
    }

    public Pair getPairOfType(EAnimation animation) {
        foreach (var pair in animations)
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



public enum EAnimation{ 
    Movement,
    SpecialAbility
}


[System.Serializable]
public class Pair{
    [SerializeField]
    public EAnimation aniomationType;
    [SerializeField]
    public List<AudioClip> clips;


    public AudioClip getRandomSound() {
        if (clips.Count <= 0) {
            Debug.LogWarning("the list of clips in CharacterAudio set is empty");
            return null;
        }
        return clips[Random.Range(0, clips.Count)];
    }
}
