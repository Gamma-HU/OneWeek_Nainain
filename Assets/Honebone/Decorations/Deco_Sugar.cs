using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_Sugar : Decoration
{
    [SerializeField] Attack attack;
    [SerializeField, Header("ダメージ補正値増加量/ランク")] int DMGModPerRank;
    public override void OnAttack(Honemy target, Attack atk, bool normalATK)
    {
        Attack ATK = attack;
        ATK.DMGMod += (decoStatus.rank - 1) * DMGModPerRank;
        if (normalATK)
        {
            foreach (Honemy tar in GameManager.instance.GetEnemies().Sample(2))
            {
                manju.Attack(tar, attack);

            }
        }
    }
}
