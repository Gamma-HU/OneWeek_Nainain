using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_WhippedCream_Pjtl : Projectile
{
    public override void OnHit(Honemy hit)
    {
        hit.Slow();
    }
}
