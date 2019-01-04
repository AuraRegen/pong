using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPanelScript : MonoBehaviour
{

    public Animator animator;

    public void FadeInGame()
    {
        animator.SetTrigger("GameStart");
    }

}
