using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPUp", menuName = "powerup/Speed Up")]
public class SpeedUp: PowerUp
{
    public int amount = 10;
    public override void OnAddedToPawn(Pawn pawn)
    {
        pawn.pawnState.Speed += amount;
    }

}
