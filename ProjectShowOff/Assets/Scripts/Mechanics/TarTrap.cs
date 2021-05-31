using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TarTrap : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,100.0f)]
    float slowPercent;


    [Header("Character skill disable options")]
    [SerializeField]
    bool disableRabbit;
    [SerializeField]
    bool disableFox;
    [SerializeField]
    bool disableBear;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter !=null) {
            charachter.Slow(slowPercent);
            if (charachter is BearController && disableBear) { charachter.DisableSkills(); }
            if (charachter is FoxController && disableFox) { charachter.DisableSkills(); }
            if (charachter is RabbitController && disableRabbit) { charachter.DisableSkills(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter != null)
        {
            charachter.ReverseSlow(slowPercent);
            if (charachter is BearController && disableBear) { charachter.EnableSkills(); }
            if (charachter is FoxController && disableFox) { charachter.EnableSkills(); }
            if (charachter is RabbitController && disableRabbit) { charachter.EnableSkills(); }
        }
    }
}
