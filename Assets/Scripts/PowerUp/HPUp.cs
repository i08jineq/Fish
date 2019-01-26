using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUp : PowerUp
{
    public override void OnAddedToPawn(Pawn pawn)
    {
        pawn.pawnState.Hp += 100f;
    }
}
