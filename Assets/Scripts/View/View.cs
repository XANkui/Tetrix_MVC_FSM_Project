using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {

    private Ctrl ctrl;

    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private RectTransform restartButton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject mute;
    private GameObject rankUI;

    private Text scoreText;
    private Text highScoreText;
    private Text gameOverScoreText;

    private Text scoreRankText;
    private Text highScoreRankText;
    private Text numbersGameRankText;

    void Awake() {

        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();

        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        restartButton = transform.Find("Canvas/MenuUI/RestartButton") as RectTransform;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;


        highScoreText = transform.Find("Canvas/GameUI/HighestScoreText/Text").GetComponent<Text>();
        scoreText = transform.Find("Canvas/GameUI/ScoreText/Text").GetComponent<Text>();
        gameOverScoreText = transform.Find("Canvas/GameOverUI/ScoreText").GetComponent<Text>();

        highScoreRankText = transform.Find("Canvas/RankUI/HighestScoreText/ScoreText").GetComponent<Text>();
        scoreRankText = transform.Find("Canvas/RankUI/CurrentScoreText/ScoreText").GetComponent<Text>();
        numbersGameRankText = transform.Find("Canvas/RankUI/GameNumberText/ScoreText").GetComponent<Text>();

        Debug.Log("logoName:" + logoName + "  menuUI:" + menuUI);
    }

    public void ShowMenu() {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-74.1f,0.5f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(59.7f, 0.5f);
    }

    public void HideMenu()
    {
       
        logoName.DOAnchorPosY(74.1f, 0.5f).OnComplete(delegate{ logoName.gameObject.SetActive(false); });
       
        menuUI.DOAnchorPosY(-59.7f, 0.5f).OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }

    public void UpdateGameUI(int score , int highScore) {
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void ShowGameUI(int score = 0, int highScore = 0)
    {
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-115.8f, 0.5f);
        
    }

    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(115.8f, 0.5f).OnComplete(delegate { gameUI.gameObject.SetActive(false); });

    }

    public void ShowRestartButton() {
        restartButton.gameObject.SetActive(true);
    }

    public void ShowGameOverUI(int score) {
        gameOverUI.SetActive(true);
        gameOverScoreText.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    

    public void OnGameOverHomeButtonClick()
    {
        // 重新加载当前场景，实现回到主页面面即可
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void OnSettingButtonClick() {
        settingUI.SetActive(true);
        ctrl.audioManager.PlayCusor();
    }

    public void HideSettingUI()
    {
        settingUI.SetActive(false);
    }


    public void MuteSetActive(bool isActive) {
        mute.SetActive(isActive);
    }

    public void UpdateRankRecordUI(int score, int highScore, int numbersGame) {
        scoreRankText.text = score.ToString();
        highScoreRankText.text = highScore.ToString();
        numbersGameRankText.text = numbersGame.ToString();

        rankUI.SetActive(true);
    }
    public void HideRankUI()
    {
        rankUI.SetActive(false);
    }


}
