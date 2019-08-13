using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

    [HideInInspector]
    public Model model;             // Model 组件
    [HideInInspector]
    public View view;               // View 组件
    [HideInInspector]
    public CameraManager cameraManager;     // 相机管理脚本
    [HideInInspector]
    public GameManager gameManager;     // 游戏管理脚本
    [HideInInspector]
    public AudioManager audioManager;     // 声音管理脚本

    private FSMSystem fsmSystem;


    void Awake() {

        // 获取 Model和View组件
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraManager = this.GetComponent<CameraManager>();
        gameManager = this.GetComponent<GameManager>();
        audioManager = this.GetComponent<AudioManager>();

        Debug.Log("model:" + model + "  view:"+ view);
    }


    // Use this for initialization
    void Start () {
        MakeFSM();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// FSM 有限状态机初始化
    /// </summary>
    private void MakeFSM()
    {
        fsmSystem = new FSMSystem();
        FSMState[] fsmStates = this.GetComponentsInChildren<FSMState>();
        foreach (FSMState state in fsmStates)
        {
            fsmSystem.AddState(state, this);
        }
        MenuState menuState = this.GetComponentInChildren<MenuState>();
        if (menuState != null)
        {
            fsmSystem.SetDefaultState(menuState);

        }
        else
        {
            Debug.Log("没有找到 MenuState 组件，请确认...");
        }

    }
}
