using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinScript : MonoBehaviour {

    public Animator animator;

    public void FadeYouWinPanel()
    {
        animator.SetTrigger("GameOver");
    }

}
