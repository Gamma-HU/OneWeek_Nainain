using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco_MiniManju : Decoration
{
    public override void OnInit()
    {
        manju.Status().exPellet++;
    }
}
