using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



[System.Serializable]
public class ImageScore {
    [SerializeField]
    public GameObject scoreImage;

    [SerializeField]
    public int score;


    public void show() {
        scoreImage.SetActive(true);
    }

    public void hide()
    {
        scoreImage.SetActive(false);
    }
}
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


    [SerializeField]
    private ImageScore[] ScoreImages;

    private void Awake()
    {
        data = PlayerData.Load(Application.dataPath + Player.FilePath);
        Debug.Log(data.trashCollectected);
        scoreBoard = ScoreBoard.LoadScore(Application.dataPath + ScoreBoard.filePath);
        scoreBoard.AddScore(data);
        scoreBoard.SaveScore(Application.dataPath + ScoreBoard.filePath);
        ShowRightImage();

    }

    public void ShowRightImage()
    {
        //Debug.LogErro
        for (int i = 0; i < ScoreImages.Length; i++) {
            ScoreImages[i].hide();
        }
        for (int i = ScoreImages.Length - 1; i >= 0; i--)
        {
            if (ScoreImages[i].score <= data.trashCollectected) {
                ScoreImages[i].show();
                break;
            }
        }
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
