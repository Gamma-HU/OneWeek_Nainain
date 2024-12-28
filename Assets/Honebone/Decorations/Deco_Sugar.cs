using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_Sugar : Decoration
{
    [SerializeField] Attack attack;
    public override void OnAttack(Enemy target, Attack atk, bool normalATK)
    {
        if (normalATK)
        {
            foreach (Enemy tar in Honebone_Test.instance.GetEnemies().Sample(2))
            {
                manju.Attack(tar, attack);

            }
        }
    }
}
