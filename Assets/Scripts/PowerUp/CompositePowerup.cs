using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Composite Powerup", menuName = "powerup/Composite Powerup")]
public class CompositePowerup : PowerUp
{

    public List<PowerUp> powerups = new List<PowerUp>();

    public override void OnAddedToPawn(Pawn pawn)
    {
        foreach(var item in powerups)
        {
            item.OnAddedToPawn(pawn);
        }
    }
}
