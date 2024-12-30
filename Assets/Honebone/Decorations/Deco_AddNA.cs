using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_AddNA : Decoration
{
    [SerializeField, Header("追加攻撃を行う確率")] int chance;
    [SerializeField, Header("追加攻撃の詳細")] Attack attack;
    [SerializeField, Header("攻撃確率増加量/ランク")] int chancePerRank;
    [SerializeField, Header("ダメージ補正値増加量/ランク")] int DMGModPerRank;
    [SerializeField, Header("発射弾数増加量/ランク")] int pelletPerRank;

    //public override void OnInit()
    //{
    //    manju.AddNormalAttack(attack);
    //}

    public override void OnAttack(Honemy target, Attack atk, bool normalATK)
    {
        Attack ATK = attack;
        ATK.DMGMod += (decoStatus.rank-1) * DMGModPerRank;
        ATK.pellets += (decoStatus.rank - 1) * pelletPerRank;
        if (normalATK && (chance + chancePerRank * (decoStatus.rank - 1)).Dice())
        {
            Honemy tar = target;
            switch (ATK.targetType)
            {
                case global::Attack.TargetType.randomTarget:
                    Debug.Log("これは一時的な処理です");
                    tar = GameManager.instance.GetEnemies().Choice();
                    break;
                case global::Attack.TargetType.highestHP:
                    Debug.Log("これは一時的な処理です");
                    break;
            }

            manju.Attack(tar, ATK);
        }
    }
}
