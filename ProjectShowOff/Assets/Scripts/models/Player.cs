using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int score;
    public void AddScore(int amount) {
        score += amount;
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

    public void Die() {
        onDeath?.Invoke();
    }

    private void Start()
    {
        onDeath.AddListener(RestartLevel);
    }

    public void SwitchCharacter() {
        ControlledCharacter.Move(Vector3.zero);
        selectedCharacterId = (selectedCharacterId + 1) % characterControllers.Count;
        
        onSwitchCharacter?.Invoke();
    }

    private void OnDestroy()
    {
        onDeath.RemoveListener(RestartLevel);
    }
}
