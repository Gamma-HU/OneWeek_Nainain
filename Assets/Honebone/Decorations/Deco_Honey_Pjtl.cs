using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_Honey_Pjtl : Projectile
{
    public override void OnHit(Honemy hit)
    {
        hit.Stun();
    }
}
