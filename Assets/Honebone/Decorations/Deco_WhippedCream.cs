using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_WhippedCream : Decoration
{
    [SerializeField] Attack attack;
    public override void OnInit()
    {
        manju.AddNormalAttack(attack);
    }
}
