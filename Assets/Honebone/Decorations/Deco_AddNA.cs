using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_AddNA : Decoration
{
    [SerializeField, Header("�ǉ��U�����s���m��")] int chance;
    [SerializeField, Header("�ǉ��U���̏ڍ�")] Attack attack;
    [SerializeField, Header("�U���m��������/�����N")] int chancePerRank;
    [SerializeField, Header("�_���[�W�␳�l������/�����N")] int DMGModPerRank;
    [SerializeField, Header("���˒e��������/�����N")] int pelletPerRank;

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
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
                    tar = GameManager.instance.GetEnemies().Choice();
                    break;
                case global::Attack.TargetType.highestHP:
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
                    break;
            }

            manju.Attack(tar, ATK);
        }
    }
}
