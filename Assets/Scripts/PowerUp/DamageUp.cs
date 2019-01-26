using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : PowerUp
{
    public override void OnAddedToPawn(Pawn pawn)
    {
        pawn.pawnState.Damage += 10f;
    }
}
