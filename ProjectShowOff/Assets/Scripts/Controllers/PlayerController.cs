using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour, ICharacterController
{
    Player playerModel;

   

    void Start() {
       playerModel = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Escape))
        {
            playerModel.TogglePause();
        }

        if (Player.IsGamePaused) return;


        Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        Move(moveDirection.normalized);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpecialAction();
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            ReleaseSpecialAction();
        }

        if (Input.GetMouseButtonDown(1)) {
            playerModel.SwitchCharacter();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            ToggleAudio();
        }

       
    }

    public void ToggleAudio() {
        playerModel.ToggleAudio();
        //playerModel.AudioEnabled = !playerModel.AudioEnabled;
        //if (playerModel.AudioEnabled)
        //{
        //    AudioListener.volume = 1;
        //} else {
        //    AudioListener.volume = 0;
        //}
    }

    public void Move(Vector3 direction)
    {
        playerModel.ControlledCharacter?.Move(direction);
    }

    public void SpecialAction()
    {
        playerModel.ControlledCharacter.SpecialAction();
    }

    public void ReleaseSpecialAction() {
        playerModel.ControlledCharacter.ReleaseSpecialAction();
    }
}
