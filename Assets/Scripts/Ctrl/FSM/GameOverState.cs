using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : FSMState {

    void Awake()
    {
        stateID = StateID.GameOver;
    }
}
