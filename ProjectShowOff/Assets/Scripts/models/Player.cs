using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int score;

    int checkPoints;
    int numberOfCheckpointsReached;
    public UnityEvent onCheckPointReached;
    public float getCheckPointProgress()
    {
        return (float)numberOfCheckpointsReached / checkPoints;
    }
    public void nextCheckPointReached()
    {
        numberOfCheckpointsReached++;
        onCheckPointReached?.Invoke();
    }



    int trashCount;
    int trashCollectedCount;
    public UnityEvent onTrashCollected;
    public float getTrashPercent() {
        return (float)trashCollectedCount / trashCount;
    }
    public void AddTrashCollectedCount(int amount = 1) {
        trashCollectedCount += amount;
        onTrashCollected?.Invoke();
    }

   


    [SerializeField]
    public static string FilePath = "/Data/player.data";


    public void AddScore(int amount) {
        score += amount;
        playerData.trashCollectected += amount;
    }


    PlayerData playerData;


    public void SaveProgress() {
        playerData.SavePlayerData(Application.dataPath + FilePath);
    }

    void Awake(){
        checkPoints = FindObjectsOfType<CheckPoint>().Length;
        playerData = PlayerData.Load(Application.dataPath + FilePath);
        trashCount = FindObjectsOfType<Pickup>().Length;
    }

    public bool AudioEnabled { get; set; } = false;

    public void ToggleAudio() {
        AudioEnabled = !AudioEnabled;
        //FMODUnity.RuntimeManager.MuteAllEvents(AudioEnabled);
        if (AudioEnabled)
        {
            
            AudioListener.volume = 1;
            
            //FMODUnity.StudioListener studioListener;
            //studioListener.
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    [HideInInspector]
    public static bool IsGamePaused { get;private set; } = false;

    public UnityEvent onGamePaused;
    public UnityEvent onGameUnPaused;
    public void TogglePause() {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            onGamePaused?.Invoke();
        }
        else {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            onGameUnPaused?.Invoke();
        }
    }

    [SerializeField]
    List<CharachterModel> characterControllers;
    public IList<CharachterModel> ControlledCharacters {
        get { return characterControllers.AsReadOnly(); }
    }
    int selectedCharacterId = 0;

    [SerializeField]
    public UnityEvent onSwitchCharacter;
    public CharachterModel ControlledCharacter { 
        get { return characterControllers[selectedCharacterId]; } 
        private set { } 
    }
    public UnityEvent onDeath;


    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCheckPoint() {
        CheckPoint.LoadProgress();
    }

    public void Die() {
        onDeath?.Invoke();
    }

    private void Start()
    {
        onDeath.AddListener(LoadCheckPoint);
        ControlledCharacter?.CharacterSeleted();
    }

    public void SwitchCharacter() {
        ControlledCharacter.Move(Vector3.zero);

        ControlledCharacter?.CharacterDeselected();
        selectedCharacterId = (selectedCharacterId + 1) % characterControllers.Count;
        ControlledCharacter?.CharacterSeleted();

        onSwitchCharacter?.Invoke();
    }

    private void OnDestroy()
    {
        onDeath.RemoveListener(LoadCheckPoint);
    }
}
