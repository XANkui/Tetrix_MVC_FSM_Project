using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : FSMState
{

    void Awake()
    {
        stateID = StateID.Pause;
    }
}
