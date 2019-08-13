using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {

    void Awake()
    {
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick,StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        // 菜单出现
        ctrl.view.ShowMenu();

        // 相机视角大小变化
        ctrl.cameraManager.ZoomOut();
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }

    public void OnStartButtonClick() {
        //播放按钮点击音效
        ctrl.audioManager.PlayCusor();

        fsm.PerformTransition(Transition.StartButtonClick);
    }

    public void OnRankButtonClick()
    {
        //播放按钮点击音效
        ctrl.audioManager.PlayCusor();
        ctrl.view.UpdateRankRecordUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }

    public void OnClearButtonClick() {
        ctrl.model.ClearData();

        // 调用 OnRankButtonClick 更新RanKUI数据显示
        OnRankButtonClick();
    }

    public void OnRestartButtonClick() {
        // 清空生成的 shape
        ctrl.gameManager.DestroyCurrentShape();
        ctrl.model.RestartGame();

        // 开始游戏，从 MenuState 转到 PlayState
        fsm.PerformTransition(Transition.StartButtonClick);
    }
}
