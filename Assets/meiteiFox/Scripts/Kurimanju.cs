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
        [Header("�U����/s")] public float attackSpeed;

        [Header("\n\n�ȉ��͑�������")]
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
    [SerializeField,Header("�ʏ�U�����")] public Attack attack;
    [SerializeField] GameObject smoke;
    Enemy target;
    //public struct DecoParams

    bool active = false;//�E�F�[�u���̂�true
    float timer;

    /// <summary>������(=�z�u���ɌĂ΂��)</summary>
    public void Init(Vector2 pos)
    {
        status.position = pos;
        Instantiate(smoke, transform);
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
        if (active)//wave���Ȃ�
        {
            if (target == null)//�^�[�Q�b�g�����݂��Ȃ��Ȃ�
            {
                SetTarget();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= (1f / status.attackSpeed))//�ʏ�U��
                {
                    Debug.Log("attack");

                    timer -= (1f / status.attackSpeed);
                    Attack(target, attack);//���ݑΏۂƂ��Ă���G�ɒʏ�U��
                }
            }
        }
    }

    void SetTarget()//�^�[�Q�b�g�ݒ�
    {
        target = Honebone_Test.instance.GetEnemy();
    }
    /// <summary>
    /// �U�����s�� �����i�̌��ʂōU������Ƃ���������Ă�
    /// </summary>
    /// <param name="tar">�U���Ώ�</param>
    /// <param name="atk">�U�����</param>
    public void Attack(Enemy tar, Attack atk)//�U��
    {
        //if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }
        if (atk.pellets == 0) { Debug.Log("���˒e����0�ł��I"); }
        if (atk.projectileDuration <= 0) { Debug.Log("�������Ԃ�0�ł��I"); }

        Vector3 dir = new Vector3();
        dir = (tar.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = atk.spread / -2f;
        for (int i = 0; i < atk.pellets; i++)
        {
            float spread = 0f;
            if (atk.spread > 0 && !atk.equidistant) { spread = Random.Range(atk.spread / -2f, atk.spread / 2f); }//�g�U�̌���
            if (atk.equidistant)//���Ԋu�ɔ��˂���Ȃ�
            {
                spread = delta;
                delta += atk.spread / (atk.pellets - 1);
            }
            if (atk.fireRandomly) { spread = Random.Range(-180f, 180f); }//�����_���ɔ�΂��Ȃ�

            var pjtl = Instantiate(atk.projectile, transform.position, quaternion);//pjtl�̐���
            pjtl.GetComponent<Projectile>().Init(this, tar, atk);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//�g�U����]������
        }
    }
}
[System.Serializable]
public struct Attack
{
    public GameObject projectile;

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
