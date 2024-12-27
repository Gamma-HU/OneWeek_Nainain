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
        [Header("攻撃回数/s")] public float attackSpeed;

        [Header("\n\n以下は代入される")]
        public float ATK_mul = 100f;
        public float range_mul = 100f;
        [Header("=ATK_base x ATK_mul(%)")] public int ATK;
        [Header("=range_base x range_mul(%)")] public float range;

        public ManjuStatus()
        {
            ATK = (ATK_base * ATK_mul / 100f).ToInt();
            range = range_base * range_mul / 100f;
        }
    }
    [SerializeField] ManjuStatus status;
}
