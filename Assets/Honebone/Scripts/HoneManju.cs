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
        Vector2 position;

        public ManjuStatus(Vector2 pos)
        {
            ATK = (ATK_base * ATK_mul / 100f).ToInt();
            range = range_base * range_mul / 100f;

            position = pos;
        }
    }
    [SerializeField] ManjuStatus status;
    [SerializeField] GameObject smoke;
    Enemy target;
    //public struct DecoParams

    bool active = false;//ウェーブ中のみtrue
    float timer;

    /// <summary>生成時(=配置時に呼ばれる)</summary>
    public void Init(Vector2 pos)
    {
        status = new ManjuStatus(pos);
        Instantiate(smoke, transform);
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
        target = Honebone_Test.instance.GetEnemy();
    }
    void Attack()//攻撃
    {

    }
}
