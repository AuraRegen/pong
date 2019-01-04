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
        
        if (gameOver && Input.anyKey)
        {
            SceneManager.LoadScene("GameScene");
        }
	}

    /*
    * Increase the score of the player
    * according to the wall that as been it
    * and end the game if the max score as been reach
    */
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
        this.ballControl.EndGame();
        this.youWin.GetComponent<YouWinScript>().FadeYouWinPanel();
    }

}
