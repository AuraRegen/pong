using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    public Text leftPlayerText;
    public Text rightPlayerText;
    public GameObject youWin;

    public GUISkin layout;

    private BallControl ballControl;

    // Use this for initialization
    void Start () {
        GameObject theBall = GameObject.FindGameObjectWithTag("Ball");
        this.ballControl = theBall.GetComponent<BallControl>();
        this.ballControl.setGameManager(this);
        this.leftPlayerText.text = "0";
        this.rightPlayerText.text = "0";
        
    }
	
	// Update is called once per frame
	void Update () {
		
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
        return PlayerScore1 == 3 || PlayerScore2 == 3;
    }

    private void endGame()
    {
        this.ballControl.ResetBall();
        this.youWin.GetComponent<YouWinScript>().FadeYouWinPanel();
    }

}
