using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int PlayerScore1;
    public static int PlayerScore2;

    public Text leftPlayerText;
    public Text rightPlayerText;
    public GameObject youWin;

    public GUISkin layout;

    private BallControl ballControl;

    private bool gameOver;

    public int maxScore = 5;

    public GameObject blackPanel;

    // Use this for initialization
    void Start () {
        GameObject theBall = GameObject.FindGameObjectWithTag("Ball");
        this.ballControl = theBall.GetComponent<BallControl>();
        this.ballControl.setGameManager(this);

        PlayerScore1 = 0;
        PlayerScore2 = 0;
        this.leftPlayerText.text = PlayerScore1.ToString();
        this.rightPlayerText.text = PlayerScore2.ToString();
        this.gameOver = false;
        this.blackPanel.GetComponent<BlackPanelScript>().FadeInGame();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("game over" + gameOver + "|" + "input anyKey" + Input.anyKey);
        if (gameOver && Input.anyKey)
        {
            SceneManager.LoadScene("GameScene");
        }
	}

    public void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            PlayerScore1++;
        }
        else if (wallID == "LeftWall")
        {
            PlayerScore2++;
        }

        if (this.gameIsOver())
        {
            this.endGame();
        } else
        {
            this.ballControl.RestartGame();
        }
        
    }

    void OnGUI()
    {
        this.leftPlayerText.text = GameManager.PlayerScore1.ToString();
        this.rightPlayerText.text = GameManager.PlayerScore2.ToString();
    }

    public bool gameIsOver()
    {
        return PlayerScore1 == maxScore || PlayerScore2 == maxScore;
    }

    private void endGame()
    {
        this.gameOver = true;
        this.ballControl.ResetBall();
        this.youWin.GetComponent<YouWinScript>().FadeYouWinPanel();
    }

}
