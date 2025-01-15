using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SinryakuStart : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform textRectTransform;
    public void Sinryaku_Start()
    {
        if (gameManager.currentPhase == 0)
        {
            gameManager.Sinryaku_Start();
            SwitchText();
        }
    }
    public void SwitchText()
    {
        textRectTransform.DOLocalMoveX(290f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            textRectTransform.localPosition = new Vector3(-290f, 0f, 0f);
            if (gameManager.currentPhase == 0)
            {
                text.text = "侵略開始！！";
            }
            else
            {
                text.text = "侵略中……";
            }
            textRectTransform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.3f).OnComplete(() =>
            {
                Debug.Log("Switched Text");
            });
        });
    }
    public void SwitchText2()
    {
        textRectTransform.DOLocalMoveX(290f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            textRectTransform.localPosition = new Vector3(-290f, 0f, 0f);
            if (gameManager.currentPhase == GameManager.Phase.Invasion)
            {
                text.text = "侵略完了！";
            }
            else
            {
                text.text = "侵略開始！！";
            }
            textRectTransform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.3f).OnComplete(() =>
            {
                Debug.Log("Switched Text");
            });
        });
    }

}
