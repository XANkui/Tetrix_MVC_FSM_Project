using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    public const int NORMAL_ROWS = 20; // 网格的标准row
    public const int Max_ROWS = NORMAL_ROWS + 3; // 20 + 3， 高度上本是 20，加 3 是为了判断是否GameOver的
    public const int Max_COLUMNS = 10;

    private Transform[,] map = new Transform[Max_COLUMNS,Max_ROWS];


    private int score = 0;
    private int higeScore = 0;
    private int numbersGame = 0;

    public int Score { get { return score; } }
    public int HighScore { get { return higeScore; } }
    public int NumbersGame { get { return numbersGame; } }

    [HideInInspector]
    public bool isUpdateScore = false;

    void Awake() {
        LoadData();
    }

    public bool IsValidMapPosition(Transform t) {
        foreach (Transform child in t) {
            if (child.tag != "Block") {
                continue;
            }

            // 由于 Shape 有移动变化，难免可能位置不是整数了，这里圆整，避免浮点数的情况
            Vector2 pos = child.position.Round();
            // 判断是否在地图内，判断是否位置上有图形了
            if (IsInsideMap(pos) == false) { return false; }
            if (map[(int)pos.x,(int)pos.y] != null) { return false; }
        }

        return true;
    }

    /// <summary>
    /// 判断游戏是否结束，条件是20以上的 3 行中格子中有Block
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver() {
        // 判断 20 以上的 3 行中，是否与 Block
        for (int i = NORMAL_ROWS; i< Max_ROWS;i++) {
            for (int j =0;j < Max_COLUMNS;j++) {
                if (map[j,i] !=null) {
                    // 游戏结束，增加游戏次数，和游戏数据
                    numbersGame++;
                    SaveData();

                    return true;
                }
            }
        }

        return false;
    }

    public void RestartGame() {
        // 清除地图中的Block，在map置空
        for (int i=0;i < Max_ROWS;i++) {
            for (int j = 0; j < Max_COLUMNS; j++)
            {
                if (map[j, i] != null) {
                    Destroy(map[j, i].gameObject);
                    map[j, i] = null;
                }
               
            }
        }

        // 当前分数清零
        score = 0;
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearData() {
        score = 0;
        higeScore = 0;
        numbersGame = 0;

        // 更新保存到本地的数据为当前的 0
        SaveData();
    }

    /// <summary>
    /// 把 Shape 的 Block 设置到 Map 中
    /// </summary>
    /// <param name="t"></param>
    public bool PlaceShapeToMap(Transform t) {
        foreach (Transform child in t) {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }

        // 判断地图是否有填满一行的情况，有责消除
        return CheckMap();
    }

    /// <summary>
    /// 判断单个Block是否在地图中
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool IsInsideMap(Vector2 pos) {

        return pos.x >= 0 && pos.x < Max_COLUMNS && pos.y >= 0;
    }

    /// <summary>
    /// 检测地图行上是否可以消除，有则消除
    /// </summary>
    private bool CheckMap() {
        //用来检测是否有消除的标记
        int count = 0;
        //一行一行的检查
        for (int i =0; i < Max_ROWS; i++) {
            bool isFull = CheckIsRowFull(i);

            if (isFull == true) {

                count++;

                DeleteRowBlock(i);

                // 该行上面的行降下来
                MoveDownRowsAbove(i + 1);

                //由于消除降下来了，所以把降到改行的 i--，再次判断一下是否填满
                i--;
            }

            
        }

        // 更新分数
        score += count * 100;
        if (score >= higeScore) {
            higeScore = score;
        }

        bool isCount = count > 0 ? true : false;
        isUpdateScore = isCount;
        return isCount;
    }

    /// <summary>
    /// 检测改行是否填满Block
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private bool CheckIsRowFull(int row) {
        for (int i =0;i < Max_COLUMNS; i++) {
            // 如果该行有一个 null，则表示没有填满
            if (map[i, row] == null) return false;
        }

        // 遍历完一行，没有 null,，表示已满
        return true;
    }

    /// <summary>
    /// 销毁该行的Block，并置空
    /// </summary>
    private void DeleteRowBlock(int row) {
        for (int i =0;i < Max_COLUMNS; i++) {
            Destroy(map[i,row].gameObject);
            map[i, row] = null;
        }
    }

    /// <summary>
    /// 把改行以上的所以Block 降下来
    /// </summary>
    /// <param name="row"></param>
    private void MoveDownRowsAbove(int row) {
        for (int i =row; i< Max_ROWS;i++) {
            MoveDowRow(i);
        }
    }

    /// <summary>
    /// 把上面的block降一行（因为一格为1，所以降一行，就是加个 -1，即可）
    /// </summary>
    /// <param name="row"></param>
    private void MoveDowRow(int row) {
        for (int i =0; i< Max_COLUMNS; i++) {
            map[i, row - 1] = map[i, row];
            map[i, row] = null;
            if (map[i, row - 1] != null) {
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    /// <summary>
    /// 加载之前保存的数据
    /// </summary>
    private void LoadData() {
        higeScore = PlayerPrefs.GetInt("HighScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumbersGame", 0);
    }

    /// <summary>
    /// 保存玩家数据
    /// </summary>
    private void SaveData() {
        PlayerPrefs.SetInt("HighScore",higeScore);
        PlayerPrefs.SetInt("NumbersGame",numbersGame);
    }


}
