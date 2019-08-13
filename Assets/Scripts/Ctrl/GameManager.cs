using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isPause = true; //游戏是否暂停
    private Shape currentShape = null;
    public Shape[] shapes;
    public Color[] colors;

    private Transform blockHolder;

    private Ctrl ctrl;


    void Awake() {
        ctrl = this.GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isPause == true){
            return;
        }
        if (currentShape == null) {
            SpawnShape();
        }

	}


    public void StartGame() {
        isPause = false;

        if (currentShape != null) {
            // 当前 SHape 继续下落
            currentShape.Resume();
        }
        
    }

    public void PauseGame()
    {
        isPause = true;

        // 当前 Shape 停止下落
        if (currentShape != null) {
            currentShape.Pause();

        }
    }

    /// <summary>
    /// 置空 currentShape ，实现开始下落下一个 Shape
    /// </summary>
    public void FallDown() {
        // 判断游戏是否结束，结束则暂停游戏，并弹出 GameOverUI，显示分数
        

        currentShape = null;

        //更新游戏分数
        if (ctrl.model.isUpdateScore == true) {
            ctrl.view.UpdateGameUI(ctrl.model.Score,ctrl.model.HighScore);
        }

        //销毁Shape的没有Block的空Shape(遍历 blockHolder 下的子物体，子物体数量小于等于1（1为Pivot）则可以销毁)
        foreach (Transform t in blockHolder) {
            if (t.childCount <= 1) {
                Destroy(t.gameObject);
            }
        }


        // 判断游戏是可以结束了
        if (ctrl.model.IsGameOver() == true)
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }

    /// <summary>
    /// 清空当前的Shape(用于重新开始游戏的时候，currentShape 没有在map中)
    /// </summary>
    public void DestroyCurrentShape() {
        Destroy(currentShape.gameObject);
        currentShape = null;
    }

    void SpawnShape() {
        int index = Random.Range(0,shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        //生成游戏BlockShape，并且父亲物体时BlockHolder
        currentShape = Instantiate(shapes[index], blockHolder);
        currentShape.SetColor(colors[indexColor], ctrl, this);
    }
}
