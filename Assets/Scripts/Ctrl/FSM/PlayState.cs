using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayState : FSMState
{
    void Awake() {
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick,StateID.Menu);
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowGameUI(ctrl.model.Score, ctrl.model.HighScore);
        ctrl.cameraManager.ZoomIn();
        ctrl.gameManager.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideGameUI();
        ctrl.view.ShowRestartButton();
        ctrl.gameManager.PauseGame();
    }

    public void OnPauseButtonClick()
    {
        //播放按钮点击音效
        ctrl.audioManager.PlayCusor();

        fsm.PerformTransition(Transition.PauseButtonClick);

    }

    public void OnGameOverRestartGameButtonClick()
    {
        ctrl.view.HideGameOverUI();        
        ctrl.model.RestartGame();
        ctrl.gameManager.StartGame();
        ctrl.view.UpdateGameUI(0,ctrl.model.HighScore);
        
    }

    
}
