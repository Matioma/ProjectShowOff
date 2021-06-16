using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreenModel : MonoBehaviour
{
    IntroView introView;

    private void Awake()
    {
        introView = FindObjectOfType<IntroView>();
    }


    public void StartGame(string sceneName) {
        PlayerData playerData = new PlayerData();
        playerData.Name = introView.Input.text;
        playerData.SavePlayerData(Player.FilePath);
        SceneManager.LoadScene(sceneName);
    }
}
