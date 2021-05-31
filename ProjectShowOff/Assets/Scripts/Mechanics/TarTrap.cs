using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TarTrap : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,100.0f)]
    float slowPercent;


    [SerializeField]
    bool disablesSkills;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter !=null) {
            charachter.Slow(slowPercent);
            if (disablesSkills) { charachter.DisableSkills(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter != null)
        {
            charachter.ReverseSlow(slowPercent);
            if (disablesSkills) { charachter.EnableSkills(); }
        }
    }
}
