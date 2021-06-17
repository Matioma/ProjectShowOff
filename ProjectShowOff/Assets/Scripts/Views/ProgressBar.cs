using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    [Range(0f,1f)]
    float progress;


    public float Progress {
        get { return progress; }
        set {
            if (value == progress) return;

            progress = value;
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;
            updateProgress();
        }
    }

    [SerializeField]
    Image ProgressMaskImage;

    private void Start()
    {
        ProgressMaskImage.fillAmount = progress;
    }



    void updateProgress()
    {
        ProgressMaskImage.fillAmount = progress;
    }
    
}
