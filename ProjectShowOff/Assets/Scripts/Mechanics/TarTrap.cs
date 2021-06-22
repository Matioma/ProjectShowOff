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



    [Header("Slowing")]
    [SerializeField]
    bool SlowFox;
    [SerializeField]
    bool SLowRabbit;
    [SerializeField]
    bool SLowBear;


    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter !=null) {
            //charachter.Slow(slowPercent);

            if (charachter is FoxController) { 
                if(disableFoxSkill) charachter.DisableSkills();
                if (SlowFox) charachter.Slow(slowPercent);
            
            }
            if (charachter is RabbitController) { 
                if (disableRabbitSkill) charachter.DisableSkills();
                if (SLowRabbit) charachter.Slow(slowPercent);
            }
            if (charachter is BearController) { 
                if (disableBearSkill) charachter.DisableSkills();
                if (SLowBear) charachter.Slow(slowPercent);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharachterModel charachter = other.GetComponent<CharachterModel>();
        if (charachter != null)
        {
            //charachter.ReverseSlow(slowPercent);

            if (charachter is FoxController)
            {
                if (disableFoxSkill) charachter.EnableSkills();
                if (SlowFox) charachter.ReverseSlow(slowPercent);

            }
            if (charachter is RabbitController)
            {
                if (disableRabbitSkill) charachter.EnableSkills();
                if (SLowRabbit) charachter.ReverseSlow(slowPercent);
            }
            if (charachter is BearController)
            {
                if (disableBearSkill) charachter.EnableSkills();
                if (SLowBear) charachter.ReverseSlow(slowPercent);
            }

            //if (charachter is FoxController) { if (disableFoxSkill) charachter.EnableSkills(); }
            //if (charachter is RabbitController) { if (disableRabbitSkill) charachter.EnableSkills(); }
            //if (charachter is BearController) { if (disableBearSkill) charachter.EnableSkills(); }
        }
    }
}
