using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreenModel : MonoBehaviour
{
    IntroView introView;

    private void Awake()
    {
        introView = FindObjectOfType<IntroView>();
    }


    public void StartGame() {
        PlayerData playerData = new PlayerData();
        playerData.Name = introView.Input.text;
        playerData.SavePlayerData(Player.FilePath);
    }
}
