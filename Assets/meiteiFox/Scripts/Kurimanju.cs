using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurimanju : MonoBehaviour
{
    [System.Serializable]
    public class ManjuStatus
    {
        public int ATK_base;
        public float range_base;
        [Header("攻撃回数/s")] public float attackSpeed;

        [Header("\n\n以下は代入される")]
        public float ATK_mul = 100f;
        public float range_mul = 100f;
        [Header("=ATK_base x ATK_mul(%)")] public int ATK;
        [Header("=range_base x range_mul(%)")] public float range;
        public Vector2 position;

        public ManjuStatus(Vector2 pos)
        {
            ATK = (ATK_base * ATK_mul / 100f).ToInt();
            range = range_base * range_mul / 100f;

            position = pos;
        }
    }

    [SerializeField] ManjuStatus status;
    [SerializeField,Header("通常攻撃情報")] public Attack attack;
    [SerializeField] GameObject smoke;
    Enemy target;
    //public struct DecoParams

    bool active = false;//ウェーブ中のみtrue
    float timer;

    /// <summary>生成時(=配置時に呼ばれる)</summary>
    public void Init(Vector2 pos)
    {
        status.position = pos;
        Instantiate(smoke, transform);
    }



    /// <summary>ウェーブ開始</summary>
    public void StartBattle()
    {
        active = true;
        Debug.Log("StartBattle");
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
                if (timer >= (1f / status.attackSpeed))//通常攻撃
                {
                    Debug.Log("attack");

                    timer -= (1f / status.attackSpeed);
                    Attack(target, attack);//現在対象としている敵に通常攻撃
                }
            }
        }
    }

    void SetTarget()//ターゲット設定
    {
        target = Honebone_Test.instance.GetEnemy();
    }
    /// <summary>
    /// 攻撃を行う 装飾品の効果で攻撃するときもこれを呼ぶ
    /// </summary>
    /// <param name="tar">攻撃対象</param>
    /// <param name="atk">攻撃情報</param>
    public void Attack(Enemy tar, Attack atk)//攻撃
    {
        //if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }
        if (atk.pellets == 0) { Debug.Log("発射弾数が0です！"); }
        if (atk.projectileDuration <= 0) { Debug.Log("持続時間が0です！"); }

        Vector3 dir = new Vector3();
        dir = (tar.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = atk.spread / -2f;
        for (int i = 0; i < atk.pellets; i++)
        {
            float spread = 0f;
            if (atk.spread > 0 && !atk.equidistant) { spread = Random.Range(atk.spread / -2f, atk.spread / 2f); }//拡散の決定
            if (atk.equidistant)//等間隔に発射するなら
            {
                spread = delta;
                delta += atk.spread / (atk.pellets - 1);
            }
            if (atk.fireRandomly) { spread = Random.Range(-180f, 180f); }//ランダムに飛ばすなら

            var pjtl = Instantiate(atk.projectile, transform.position, quaternion);//pjtlの生成
            pjtl.GetComponent<Projectile>().Init(this, tar, atk);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//拡散分回転させる
        }
    }
}
[System.Serializable]
public struct Attack
{
    public GameObject projectile;

    [Header("ターゲットを追尾するか/方向転換速度")] public float followTargetSpeed;
    [Header("現在のプレイヤーの位置を追尾するか falseなら発射時の場所へ")] public bool followCurrentTarget;

    [Header("一回の発射で射出する弾数")] public int pellets;
    [Header("ランダムな方向に発射するか")] public bool fireRandomly;
    [Header("+-(spread/2)°のブレが生じる")] public float spread;
    [Header("spread上に等間隔に発射するか")] public bool equidistant;

    //[Header("発射回数")] public float fireRounds = 1;
    //[Header("発射回数が2以上の時に参照 1発発射するごとのインターバル[s] 0なら同時発射")] public float fireRate;

    [Header("x:min y:max min～maxの間でランダムに決まる")]
    public Vector2 projectileSpeed;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration;
}
