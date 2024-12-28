using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_Spice : Decoration
{
    [SerializeField] float spread;
    [SerializeField] float ASPerRank;
    int curentrank;
    public override void OnInit()
    {
        curentrank = decoStatus.rank;
        manju.Status().AddAttackSpeed(0, curentrank * ASPerRank);
    }

    public override void OnAddRank(int add)
    {
        manju.Status().AddAttackSpeed(0, curentrank * ASPerRank * -1);
        curentrank = decoStatus.rank;
        manju.Status().AddAttackSpeed(0, curentrank * ASPerRank);
    }
}
