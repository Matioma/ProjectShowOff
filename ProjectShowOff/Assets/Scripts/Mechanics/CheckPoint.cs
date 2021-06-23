using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    UnityEvent onCheckPointReached;
    Player playerModel;

    int charactersInZone = 0;

    [SerializeField]
    string targetScene;


    bool everReached = false;



    public static Dictionary<CharachterModel, Vector3> SavedPosition = new Dictionary<CharachterModel, Vector3>();


    private void Awake()
    {
       
        onCheckPointReached.AddListener(SaveProgress);
    }



    private void Start()
    {
        playerModel = FindObjectOfType<Player>();
        SaveProgress();
    }
    void CharacterEnterZone() {
        charactersInZone++;

        if (!everReached) {
            everReached = true;
            onCheckPointReached?.Invoke();
            playerModel?.nextCheckPointReached();
        }
    }
    void CharacterExitZone() {
        charactersInZone--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharachterModel>() != null) {
            CharacterEnterZone();
        }
        foreach (var character in playerModel.ControlledCharacters) {
            if (other.gameObject == character.gameObject) {
                CharacterEnterZone();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var character in playerModel.ControlledCharacters)
        {
            if (other.gameObject == character.gameObject)
            {
                CharacterExitZone();
            }
        }
    }
    void SaveProgress()
    {
        SavedPosition.Clear();
        foreach (var charcater in playerModel.ControlledCharacters) {
            SavedPosition.Add(charcater, new Vector3( charcater.transform.position.x, charcater.transform.position.y, charcater.transform.position.z));
        }
    }

    public static void LoadProgress()
    {
        foreach (var pair in SavedPosition) {
            pair.Key.ResetSelfs();
            pair.Key.GetComponent<CharacterController>().enabled = false;
            pair.Key.transform.position =  new Vector3(pair.Value.x, pair.Value.y, pair.Value.z);
            pair.Key.GetComponent<CharacterController>().enabled = true;
           
        }
    }

    public void LoadLevel()
    {
        LevelLoader.LoadLevel(targetScene);
        //SceneManager.LoadScene(targetScene);
    }

}
