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

        [Header("\n\n�ȉ��͑�������")]
        public int level;
        public float ATK_mul = 100f;
        public float range_mul = 100f;
        public float AS_mul = 100f;

        public float spread;
        [Header("=ATK_base x ATK_mul(%)")] public int ATK;
        [Header("=range_base x range_mul(%)")] public float range;
        [Header("�U����/s")] public float attackSpeed;
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
    [SerializeField,Header("�ʏ�U�����")] public Attack attack;

    List<Attack> normalAttacks=new List<Attack>();
    List<int> attackWeight = new List<int>();

    //SerializeField�Ƃ��Ă���̂́A�f�o�b�O���ɍŏ����瑕���i��҂�����������
    [SerializeField] List<DecorationParams> decorations;

    [SerializeField] GameObject smoke;

    public ManjuStatus Status() { return status; }

    Honemy target;
    //public struct DecoParams

    bool active = false;//�E�F�[�u���̂�true
    float timer;

    /// <summary>������(=�z�u���ɌĂ΂��)</summary>
    public void Init(Vector2 pos, List<DecorationParams> decos,int level)
    {

        status.Init(pos);
        //levelup
        Instantiate(smoke, transform);
        normalAttacks = new List<Attack>();
        AddNormalAttack(attack);//�f�t�H���g�̒ʏ�U�����v�[���ɒǉ�

        //foreach (DecorationParams deco in decos)
        //{
        //    DecorationParams d = new DecorationParams();//�^����ꂽ�����i���R�s�[���Ď��g�ɒǉ�
        //    d.rank = deco.rank;
        //    d.decoData = deco.decoData;
        //    decorations.Add(d);
        //}
        decorations.AddRange(new List<DecorationParams>(decos));

        for (int i = 0; i < decorations.Count; i++)//�����i���C���X�^���X��
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
            if (decorations[i].decoData == deco)//���łɑ����ς݂̃f�R���[�V�����̏ꍇ�̓����N�㏸
            {
                Debug.Log($"�����i<{decorations[i].instance.Status().decoName}>�̃����N���㏸");
                decorations[i].AddRank(rank);
                return;
            }
        }

        //�V���������̍ۂ͐V�K�쐬
        DecorationParams newDeco = new DecorationParams();
        newDeco.decoData = deco;
        newDeco.rank = rank;
        var d = Instantiate(deco, transform);
        d.GetComponent<Decoration>().Init(this, rank);
        newDeco.instance = d.GetComponent<Decoration>();

        Debug.Log($"�V���ȑ����i<{newDeco.instance.Status().decoName}>�𑕔�");
        decorations.Add(newDeco);
    }

    public void AddNormalAttack(Attack atk)
    {
        normalAttacks.Add(atk);
        attackWeight.Add(atk.weight);
    }


    /// <summary>�E�F�[�u�J�n</summary>
    public void StartBattle()
    {
        active = true;
        Debug.Log("StartBattle");
    }
    /// <summary>�E�F�[�u�I��</summary>
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
        if (active)//wave���Ȃ�
        {
            if (target == null||!target.GetComponent<Honemy>().CheckAlive())//�^�[�Q�b�g�����݂��Ȃ��Ȃ�
            {
                SetTarget();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= (1f / status.attackSpeed))//�ʏ�U��
                {
                    timer -= (1f / status.attackSpeed);
                    Attack(target, normalAttacks[attackWeight.ChoiceWithWeight()], true);//���ݑΏۂƂ��Ă���G�ɒʏ�U��                    
                }
            }
        }
    }

    void SetTarget()//�^�[�Q�b�g�ݒ�
    {
        target = GameManager.instance.GetEnemy();
    }
    /// <summary>
    /// �U�����s�� �����i�̌��ʂōU������Ƃ���������Ă�
    /// </summary>
    /// <param name="tar">�U���Ώ�</param>
    /// <param name="atk">�U�����</param>
    public void Attack(Honemy tar, Attack atk,bool normalAttack=false)//�U��
    {
        //if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }
        if (atk.pellets == 0) { Debug.Log("���˒e����0�ł��I"); }
        if (atk.projectileDuration <= 0) { Debug.Log("�������Ԃ�0�ł��I"); }
        Honemy finalTarget;
        if (!normalAttack) { finalTarget = tar; }
        else
        {
            switch (atk.targetType)
            {
                case global::Attack.TargetType.randomTarget:
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
                    finalTarget = GameManager.instance.GetEnemies().Choice();
                    break;
                case global::Attack.TargetType.highestHP:
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
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
            if (spread_fin > 0 && !atk.equidistant) { spread = Random.Range(spread_fin / -2f, spread_fin / 2f); }//�g�U�̌���
            if (atk.equidistant)//���Ԋu�ɔ��˂���Ȃ�
            {
                spread = delta;
                delta += spread_fin / (atk.pellets - 1);
            }
            if (atk.fireRandomly) { spread = Random.Range(-180f, 180f); }//�����_���ɔ�΂��Ȃ�

            var pjtl = Instantiate(atk.projectile, transform.position, quaternion);//pjtl�̐���
            pjtl.GetComponent<Projectile>().Init(this, finalTarget, atk);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//�g�U����]������
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

    [Header("�_���[�W�␳�l(%)")] public float DMGMod;
    [Header("(�ʏ�U���ɂ̂݊֘A)\n�ʏ�U�����A���ꂪ�I�΂��d��")] public int weight;


    [Header("�^�[�Q�b�g��ǔ����邩/�����]�����x")] public float followTargetSpeed;
    [Header("���݂̃v���C���[�̈ʒu��ǔ����邩 false�Ȃ甭�ˎ��̏ꏊ��")] public bool followCurrentTarget;

    [Header("���̔��˂Ŏˏo����e��")] public int pellets;
    [Header("�����_���ȕ����ɔ��˂��邩")] public bool fireRandomly;
    [Header("+-(spread/2)���̃u����������")] public float spread;
    [Header("spread��ɓ��Ԋu�ɔ��˂��邩")] public bool equidistant;

    //[Header("���ˉ�")] public float fireRounds = 1;
    //[Header("���ˉ񐔂�2�ȏ�̎��ɎQ�� 1�����˂��邲�Ƃ̃C���^�[�o��[s] 0�Ȃ瓯������")] public float fireRate;

    [Header("x:min y:max min�`max�̊ԂŃ����_���Ɍ��܂�")]
    public Vector2 projectileSpeed;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration;
}

//�\���̑�����
[System.Serializable]
public class DecorationParams
{
    public GameObject decoData;//�v���n�u�̏��
    public Decoration instance;//�C���X�^���X�������}�l�[�W���[�̃X�N���v�g���
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
