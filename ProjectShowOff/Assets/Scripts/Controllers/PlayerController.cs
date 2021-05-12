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
        Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        Move(moveDirection.normalized);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpecialAction();
        }
    }
    public void Move(Vector3 direction)
    {
        playerModel.ControlledCharacter?.Move(direction);
    }

    public void SpecialAction()
    {
        playerModel.ControlledCharacter.SpecialAction();
    }
}
