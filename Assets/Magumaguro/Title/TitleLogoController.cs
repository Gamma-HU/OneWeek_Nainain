using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleLogoController : MonoBehaviour
{
    [SerializeField] float amplitude;
    void Start()
    {
        //StartCoroutine(Motion());
        Application.targetFrameRate = 60;

        this.transform.rotation = Quaternion.Euler(0, 0, -amplitude);
        

        //Sequenceのインスタンスを作成
        //var sequence = DOTween.Sequence();

        //Appendで動作を追加していく
        /*sequence.Append(this.transform.DOMoveY(2f, 2f).SetEase(Ease.OutBounce));
        sequence.Append(this.transform.DORotate(new Vector3(0, 0, amplitude), 1f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo));*/
        this.transform.DOMoveY(2f, 2f).SetEase(Ease.OutBounce);
        this.transform.DORotate(new Vector3(0, 0, amplitude), 2f, RotateMode.Fast).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        //Playで実行
        //sequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private IEnumerator Motion()
    {
        for (int i = 0; i < 45; i++)
        {
            transform.position = transform.position + new Vector3(0, -0.1f, 0);
            yield return null;
        }
        
    }*/


}
