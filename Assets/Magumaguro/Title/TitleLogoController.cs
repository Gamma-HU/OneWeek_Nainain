using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleLogoController : MonoBehaviour
{
    [SerializeField] float amplitude;
    private Tween tween_1;
    private Tween tween_2;
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
        tween_1 = this.transform.DOMoveY(1.5f, 2f).SetEase(Ease.OutBounce).SetAutoKill(true);
        tween_2 = this.transform.DORotate(new Vector3(0, 0, amplitude), 2f, RotateMode.Fast).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetAutoKill(true);

        //Playで実行
        //sequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        if(tween_1 != null && tween_1.IsActive())
        {
            tween_1.Kill();
        }
        
        if(tween_2 != null && tween_2.IsActive())
        {
            tween_2.Kill();
        }
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
