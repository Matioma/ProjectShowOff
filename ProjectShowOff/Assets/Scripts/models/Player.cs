using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int score;

    [SerializeField]
    string FilePath = "/Data/player.data";


    public void AddScore(int amount) {
        score += amount;
        playerData.trashCollectected += amount;
    }


    PlayerData playerData;


    public void SaveProgress() {
        playerData.SavePlayerData(Application.dataPath + FilePath);
    }


    public bool AudioEnabled { get; set; } = false;


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

    public void LoadProgress() {
        CheckPoint.LoadProgress();
    }

    public void Die() {
        onDeath?.Invoke();
    }

    private void Awake()
    {
        playerData = PlayerData.Load(Application.dataPath + FilePath);
    }

    private void Start()
    {
        onDeath.AddListener(LoadProgress);
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
        onDeath.RemoveListener(LoadProgress);
    }
}
