using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ChracterStateMachine
{
    [SerializeField]
    State intialState;

   
    State activState;

    [SerializeField]
    List<NameStatePair> ObjectStates;


    public void Init()
    {
        SetState(intialState);
    }


    public void SetState(string identifier) {
        activState?.Exit();
        activState = findState(identifier);
        activState?.Enter();
    }

    public void SetState(State state) {
        activState?.Exit();
        activState = state;
        activState?.Enter();
    }

    State findState(string identifier) {
        foreach (var NameStatePair in ObjectStates) {
            if (NameStatePair.name.Equals(identifier)) {
                return NameStatePair.state;
            }
        }
        Debug.LogError("Unkown State requested");
        return null;
    }
}

[System.Serializable]
public class NameStatePair {
    public string name;
    public State state;

}