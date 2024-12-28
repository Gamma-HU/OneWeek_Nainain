using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : ScriptableObject
{
    //これはボツ
    public GameObject projectile;

    [Header("ターゲットを追尾するか/方向転換速度")] public float followTargetSpeed;
    [Header("現在のプレイヤーの位置を追尾するか falseなら発射時の場所へ")] public bool followCurrentTarget;

    [Header("一回の発射で射出する弾数")] public int pellets = 1;
    [Header("ランダムな方向に発射するか")] public bool fireRandomly;
    [Header("+-(spread/2)°のブレが生じる")] public float spread;
    [Header("spread上に等間隔に発射するか")] public bool equidistant;

    //[Header("発射回数")] public float fireRounds = 1;
    //[Header("発射回数が2以上の時に参照 1発発射するごとのインターバル[s] 0なら同時発射")] public float fireRate;

    [Header("x:min y:max min～maxの間でランダムに決まる")]
    public Vector2 projectileSpeed;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration = 1f;
}
