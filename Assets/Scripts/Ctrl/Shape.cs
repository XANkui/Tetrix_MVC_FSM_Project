using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

    private Ctrl ctrl;
    private GameManager gameManager;
    private Transform pivot;

    private bool isPause = false;
    private bool isSpeedupFall = false;

    private float stepTime = 0.8f;
    private int multiple = 15;   //加速下落的倍数
    //private float stepTime =0.1f;       // 测试使用
    private float timer = 0.0f;


    void Awake() {
        pivot = transform.Find("Pivot");
    }

    void Update() {

        if (isPause == true) { return; }

        timer += Time.deltaTime;
        if (timer >= stepTime) {
            timer -= stepTime;
            Fall();
        }
        // 控制左右移动
        InputController();
    }

    public void SetColor(Color color, Ctrl ctrl, GameManager gameManager) {
        foreach (Transform t in transform) {
            if (t.tag == "Block") {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }

        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }

    /// <summary>
    /// Shape 停止下落
    /// </summary>
    public void Pause() {
        isPause = true;
    }

    /// <summary>
    /// Shape 继续下落
    /// </summary>
    public void Resume() {
        isPause = false;
    }

    public void InputController() {

        // 加速下落的时候，停止输入操作
        if (isSpeedupFall == true) return;

        float h = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            h = -1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }
        // 左右移动的功能
        if (h != 0) {
            // 播放音效
            ctrl.audioManager.PlayBallon();

            Vector3 pos = transform.position;
            pos.x += h;
            this.transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;
            }
        }

        // 旋转的功能
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            //顺时针旋转 90 
            transform.RotateAround(pivot.position,Vector3.forward, -90);
            //旋转出去，在转回来
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else {  // 旋转成功，播放音效
                // 播放音效
                ctrl.audioManager.PlayBallon();
            }
        }

        // 加速下落
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            isSpeedupFall = true;
            stepTime /= multiple;
        }
    }

    private void Fall()
    {
        // 播放下落的音效
        ctrl.audioManager.PlayDrop();

        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        // 当前Shape移回去，暂停当前Shape下落，开始新的Shape生成下落
        if (ctrl.model.IsValidMapPosition(this.transform) == false) {
            // 当前Shape移回去
            pos.y += 1;
            this.transform.position = pos;

            // 把停止的Shape的Block数据保存到 map 中,并进行判断是否有填满的行，有责自行消除,并播放音效
            bool isLineClear =  ctrl.model.PlaceShapeToMap(this.transform);
            if (isLineClear == true) {
                ctrl.audioManager.PlayLineClear();
            }

            //暂停当前Shape下落，开始新的Shape生成下落
            isPause = true;
            gameManager.FallDown();
        }
    }
}
