using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneManju : MonoBehaviour
{

    [System.Serializable]
    public class ManjuStatus
    {
        public int ATK_base;
        public float range_base;
        [Header("弾のプレハブ")]public GameObject projectile;
        [Header("攻撃回数/s")] public float attackSpeed;

        [Header("\n\n以下は代入される")]
        public float ATK_mul = 100f;
        public float range_mul = 100f;
        [Header("=ATK_base x ATK_mul(%)")] public int ATK;
        [Header("=range_base x range_mul(%)")] public float range;
        Vector2Int position;

        public ManjuStatus(Vector2Int pos)
        {
            ATK = (ATK_base * ATK_mul / 100f).ToInt();
            range = range_base * range_mul / 100f;

            position = pos;
        }
    }
    [SerializeField] ManjuStatus status;
    Enemy target;
    //public struct DecoParams

    bool active = false;//ウェーブ中のみtrue
    float timer;

    public void Init(Vector2Int pos)
    {
        status = new ManjuStatus(pos);
    }

    

    /// <summary>ウェーブ開始</summary>
    public void StartBattle()
    {
        active = true;
    }
    /// <summary>ウェーブ終了</summary>
    public void EndBattle()
    {
        active = false;
        target = null;
        timer = 0;
    }

    private void Update()
    {
        if (active)//wave中なら
        {
            if (target == null)//ターゲットが存在しないなら
            {
                SetTarget();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= (1f / status.attackSpeed))
                {
                    timer -= (1f / status.attackSpeed);
                    Attack();
                }
            }
        }
    }

    void SetTarget()//ターゲット設定
    {
        //生成した敵を管理してるスクリプトから、攻撃範囲内にいて先頭にいる敵を取得
    }
    void Attack()//攻撃
    {

    }
}
