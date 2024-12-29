using System.Collections.Generic;
using UnityEngine;

public class Kurimanju : MonoBehaviour
{
    [System.Serializable]
    public class ManjuStatus
    {
        public int ATK_base;
        public float range_base;
        public float AS_base;

        [Header("\n\n以下は代入される")]
        public int level;
        public float ATK_mul = 100f;
        public float range_mul = 100f;
        public float AS_mul = 100f;

        public float spread;
        [Header("=ATK_base x ATK_mul(%)")] public int ATK;
        [Header("=range_base x range_mul(%)")] public float range;
        [Header("攻撃回数/s")] public float attackSpeed;
        public Vector2 position;

        public void Init(Vector2 pos)
        {
            ATK = (ATK_base * ATK_mul / 100f).ToInt();
            range = range_base * range_mul / 100f;
            attackSpeed = AS_base * AS_mul / 100f;

            position = pos;
        }

        public void AddRange(float add_base,float add_mul)
        {
            range_base += add_base;
            range_mul += add_mul;
            range = range_base * range_mul / 100f;
        }
        public void AddAttackSpeed(float add_base, float add_mul)
        {
            AS_base += add_base;
            AS_mul += add_mul;
            attackSpeed = AS_base * AS_mul / 100f;
        }
    }

    [SerializeField] ManjuStatus status;
    [SerializeField,Header("通常攻撃情報")] public Attack attack;

    List<Attack> normalAttacks=new List<Attack>();
    List<int> attackWeight = new List<int>();

    //SerializeFieldとしているのは、デバッグ時に最初から装備品を待たせたいから
    [SerializeField] List<DecorationParams> decorations;

    [SerializeField] GameObject smoke;

    public ManjuStatus Status() { return status; }

    Honemy target;
    //public struct DecoParams

    bool active = false;//ウェーブ中のみtrue
    float timer;

    /// <summary>生成時(=配置時に呼ばれる)</summary>
    public void Init(Vector2 pos, List<DecorationParams> decos,int level)
    {

        status.Init(pos);
        //levelup
        Instantiate(smoke, transform);
        normalAttacks = new List<Attack>();
        AddNormalAttack(attack);//デフォルトの通常攻撃をプールに追加

        //foreach (DecorationParams deco in decos)
        //{
        //    DecorationParams d = new DecorationParams();//与えられた装飾品をコピーして自身に追加
        //    d.rank = deco.rank;
        //    d.decoData = deco.decoData;
        //    decorations.Add(d);
        //}
        decorations.AddRange(new List<DecorationParams>(decos));

        for (int i = 0; i < decorations.Count; i++)//装飾品をインスタンス化
        {
            var d = Instantiate(decorations[i].decoData, transform);
            d.GetComponent<Decoration>().Init(this, decorations[i].rank);
            decorations[i].instance = d.GetComponent<Decoration>();
        }
    }

    public void Equip(GameObject deco,int rank)
    {
        for (int i= 0;i<decorations.Count;i++)
        {
            if (decorations[i].decoData == deco)//すでに装備済みのデコレーションの場合はランク上昇
            {
                Debug.Log($"装飾品<{decorations[i].instance.Status().decoName}>のランクを上昇");
                decorations[i].AddRank(rank);
                return;
            }
        }

        //新しい装備の際は新規作成
        DecorationParams newDeco = new DecorationParams();
        newDeco.decoData = deco;
        newDeco.rank = rank;
        var d = Instantiate(deco, transform);
        d.GetComponent<Decoration>().Init(this, rank);
        newDeco.instance = d.GetComponent<Decoration>();

        Debug.Log($"新たな装飾品<{newDeco.instance.Status().decoName}>を装備");
        decorations.Add(newDeco);
    }

    public void AddNormalAttack(Attack atk)
    {
        normalAttacks.Add(atk);
        attackWeight.Add(atk.weight);
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
        Vector2 origin = transform.position;
        Vector2 direction = new Vector2(1, 0);
        Debug.DrawRay(origin, direction * status.range, Color.red);
        if (active)//wave中なら
        {
            if (target == null||!target.GetComponent<Honemy>().CheckAlive())//ターゲットが存在しないなら
            {
                SetTarget();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= (1f / status.attackSpeed))//通常攻撃
                {
                    timer -= (1f / status.attackSpeed);
                    Attack(target, normalAttacks[attackWeight.ChoiceWithWeight()], true);//現在対象としている敵に通常攻撃                    
                }
            }
        }
    }

    void SetTarget()//ターゲット設定
    {
        target = GameManager.instance.GetEnemy();
    }
    /// <summary>
    /// 攻撃を行う 装飾品の効果で攻撃するときもこれを呼ぶ
    /// </summary>
    /// <param name="tar">攻撃対象</param>
    /// <param name="atk">攻撃情報</param>
    public void Attack(Honemy tar, Attack atk,bool normalAttack=false)//攻撃
    {
        //if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }
        if (atk.pellets == 0) { Debug.Log("発射弾数が0です！"); }
        if (atk.projectileDuration <= 0) { Debug.Log("持続時間が0です！"); }
        Honemy finalTarget;
        if (!normalAttack) { finalTarget = tar; }
        else
        {
            switch (atk.targetType)
            {
                case global::Attack.TargetType.randomTarget:
                    Debug.Log("これは一時的な処理です");
                    finalTarget = GameManager.instance.GetEnemies().Choice();
                    break;
                case global::Attack.TargetType.highestHP:
                    Debug.Log("これは一時的な処理です");
                    finalTarget = tar;
                    break;
                default:
                    finalTarget = tar;
                    break;
            }
        }

        atk.projectileDuration *= status.range_mul / 100f;

        Vector3 dir = new Vector3();
        dir = (finalTarget.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float spread_fin = Mathf.Max(atk.spread + status.spread, 0);
        float delta = spread_fin / -2f;
        for (int i = 0; i < atk.pellets; i++)
        {
            float spread = 0f;
            if (spread_fin > 0 && !atk.equidistant) { spread = Random.Range(spread_fin / -2f, spread_fin / 2f); }//拡散の決定
            if (atk.equidistant)//等間隔に発射するなら
            {
                spread = delta;
                delta += spread_fin / (atk.pellets - 1);
            }
            if (atk.fireRandomly) { spread = Random.Range(-180f, 180f); }//ランダムに飛ばすなら

            var pjtl = Instantiate(atk.projectile, transform.position, quaternion);//pjtlの生成
            pjtl.GetComponent<Projectile>().Init(this, finalTarget, atk);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//拡散分回転させる
        }

        foreach(DecorationParams decorationParams in decorations)
        {
            decorationParams.instance.OnAttack(finalTarget, atk, normalAttack);
        }
    }
}
[System.Serializable]
public struct Attack
{
    public GameObject projectile;
    public enum TargetType { front, randomTarget, highestHP }
    public TargetType targetType;

    [Header("ダメージ補正値(%)")] public float DMGMod;
    [Header("(通常攻撃にのみ関連)\n通常攻撃時、これが選ばれる重み")] public int weight;


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

//饅頭の装備状況
[System.Serializable]
public class DecorationParams
{
    public GameObject decoData;//プレハブの情報
    public Decoration instance;//インスタンス化したマネージャーのスクリプト情報
    public int rank;

    public void AddRank(int add)
    {
        rank+=add;
        instance.AddRank(add);
    }

    public void SetInstance(Decoration i)
    {
        Debug.Log("ok");
        instance = i;
    }
}
