using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (gameManager.currentPhase == GameManager.Phase.Invasion)
        {
            if (gameManager.currentWave != 1)
                animator.SetBool("IsInvation", true);

        }
        else
        {
            animator.SetBool("IsInvation", false);
        }
    }
}
