using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageUp", menuName = "powerup/Damage Up")]
public class DamageUp : PowerUp
{
    public int amount = 10;
    public override void OnAddedToPawn(Pawn pawn)
    {
        pawn.pawnState.Damage += amount;
    }
}
