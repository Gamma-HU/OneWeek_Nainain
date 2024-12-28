using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_BambooStick : Decoration
{
    [SerializeField] float rangePerRank;
    int curentrank;
    public override void OnInit()
    {
        curentrank = decoStatus.rank;
        manju.Status().AddRange(0, curentrank * rangePerRank);
    }

    public override void OnAddRank(int add)
    {
        manju.Status().AddRange(0, curentrank * rangePerRank * -1);
        curentrank = decoStatus.rank;
        manju.Status().AddRange(0, curentrank * rangePerRank);
    }
}
