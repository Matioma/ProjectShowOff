using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionScreenView : MonoBehaviour
{
    private PlayerData data;
    private ScoreBoard scoreBoard;


    [SerializeField]
    private GameObject NameList;

    [SerializeField]
    private GameObject NameTemplate;

    [SerializeField]
    private TextMeshProUGUI scoreText;


    private void Awake()
    {
        data = PlayerData.Load(Application.dataPath + Player.FilePath);
        scoreBoard = ScoreBoard.LoadScore(Application.dataPath + ScoreBoard.filePath);
        scoreBoard.AddScore(data);
        scoreBoard.SaveScore(Application.dataPath + ScoreBoard.filePath);

    }
    private void Start()
    {
        scoreBoard.players.Reverse();
        for (int i = 0; i < scoreBoard.players.Count; i++)
        {
            DisplayLeaderBoardScore(scoreBoard.players[i], i + 1);
        }
        DisplayUserScore();
    }

    private void DisplayLeaderBoardScore(PlayerData data, int index) {
        GameObject obj = Instantiate(NameTemplate, NameList.transform);
        var textCOmponent = obj.GetComponent<TextMeshProUGUI>();
        if (textCOmponent != null) {
            textCOmponent.text = $"{index}: {data.Name}: {data.trashCollectected}";
        }
    }


    private void DisplayUserScore(){
        if (scoreText == null) return;
        scoreText.text = $"{data.trashCollectected}";
    }
}
