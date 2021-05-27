using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    CharacterAudioManager characterAudioManager;

    public void TriggerStep() {
        characterAudioManager.PlayStep();
    }
}
