using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroView : MonoBehaviour
{
    [SerializeField]
    TMP_InputField input;
    [SerializeField]
    Button enterButton;


    public TMP_InputField Input { get { return input; } }


    private void Start()
    {
        input.onValueChanged.AddListener(processInputOfThePlayer);
    }

    void processInputOfThePlayer(string value) {
        enterButton.interactable = value.Length > 0;
    }
}
