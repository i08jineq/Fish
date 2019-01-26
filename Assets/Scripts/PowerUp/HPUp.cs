using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPUp", menuName = "powerup/HP Up")]
public class HPUp : PowerUp
{
    public int amount = 100;
    public override void OnAddedToPawn(Pawn pawn)
    {
        pawn.pawnState.Hp += amount;
    }
}
