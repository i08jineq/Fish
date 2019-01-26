using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : ScriptableObject
{
    public Sprite icon;
    public string powerupName = "power up";
    public string description = "give you the power";
    public virtual void OnAddedToPawn(Pawn pawn)
    {

    }
    
    public virtual void OnUpdate(float deltaTime)
    {

    }
}
