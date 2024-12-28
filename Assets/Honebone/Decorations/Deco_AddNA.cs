using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_AddNA : Decoration
{
    [SerializeField] int chance;
    [SerializeField] Attack attack;
    //public override void OnInit()
    //{
    //    manju.AddNormalAttack(attack);
    //}

    public override void OnAttack(Enemy target, Attack atk, bool normalATK)
    {
        if (normalATK&& chance.Dice())
        {
            Enemy tar = target;
            switch (attack.targetType)
            {
                case global::Attack.TargetType.randomTarget:
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
                    tar = Honebone_Test.instance.GetEnemies().Choice();
                    break;
                case global::Attack.TargetType.highestHP:
                    Debug.Log("����͈ꎞ�I�ȏ����ł�");
                    break;
            }

            manju.Attack(tar, attack);
        }
    }
}