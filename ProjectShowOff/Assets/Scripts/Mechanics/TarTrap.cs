using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TarTrap : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,100.0f)]
    float slowPercent;


    [Header("Skill disabling")]
    [SerializeField]
    bool disableFoxSkill;
    [SerializeField]
    bool disableRabbitSkill;
    [SerializeField]
    bool disableBearSkill;


    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter !=null) {
            charachter.Slow(slowPercent);

            if (charachter is FoxController) { if(disableFoxSkill) charachter.DisableSkills(); }
            if (charachter is RabbitController) { if (disableRabbitSkill) charachter.DisableSkills(); }
            if (charachter is BearController) { if (disableBearSkill) charachter.DisableSkills(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter != null)
        {
            charachter.ReverseSlow(slowPercent);

            if (charachter is FoxController) { if (disableFoxSkill) charachter.EnableSkills(); }
            if (charachter is RabbitController) { if (disableRabbitSkill) charachter.EnableSkills(); }
            if (charachter is BearController) { if (disableBearSkill) charachter.EnableSkills(); }
        }
    }
}
