using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_Sugar : Decoration
{
    [SerializeField] Attack attack;
    public override void OnAttack(Honemy target, Attack atk, bool normalATK)
    {
        if (normalATK)
        {
            foreach (Honemy tar in GameManager.instance.GetEnemies().Sample(2))
            {
                manju.Attack(tar, attack);

            }
        }
    }
}
