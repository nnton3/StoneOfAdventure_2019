using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    #region Variables
    private Animator anim;
    private NextLevelBtn nextLevelBtn;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        nextLevelBtn = FindObjectOfType<NextLevelBtn>();

        nextLevelBtn.PlayerStartNextLevel.AddListener(StartAnimation);
    }

    private void StartAnimation()
    {
        anim.SetTrigger("start");
    }
}
