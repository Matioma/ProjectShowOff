using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIndicator : MonoBehaviour
{
    [SerializeField]
    Sprite currentIndicator;
    IndicatorView indicatorCanvas;

    public void DisplayIndicator(Sprite sprite) {
        currentIndicator = sprite;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
