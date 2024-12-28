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
        [Header("�e�̃v���n�u")]public GameObject projectile;
        [Header("�U����/s")] public float attackSpeed;

        [Header("\n\n�ȉ��͑�������")]
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

    bool active = false;//�E�F�[�u���̂�true
    float timer;

    /// <summary>������(=�z�u���ɌĂ΂��)</summary>
    public void Init(Vector2 pos)
    {
        status = new ManjuStatus(pos);
        Instantiate(smoke, transform);
    }

    

    /// <summary>�E�F�[�u�J�n</summary>
    public void StartBattle()
    {
        active = true;
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
                if (timer >= (1f / status.attackSpeed))
                {
                    timer -= (1f / status.attackSpeed);
                    Attack();
                }
            }
        }
    }

    void SetTarget()//�^�[�Q�b�g�ݒ�
    {
        target = Honebone_Test.instance.GetEnemy();
    }
    void Attack()//�U��
    {

    }
}
