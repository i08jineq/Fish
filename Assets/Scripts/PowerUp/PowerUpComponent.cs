using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpComponent
{
    private Pawn ownerPawn;

    private List<PowerUp> powerUps = new List<PowerUp>();
    private int powerUpCount = 0;

    public void Init(Pawn pawn)
    {
        ownerPawn = pawn;
    }

    public void OnUpdate(float deltaTime)
    {
        for (int i = 0; i < powerUpCount; i++)
        {
            powerUps[i].OnUpdate(deltaTime);
        }
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        powerUps.Add(powerUp);
        powerUp.OnAddedToPawn(ownerPawn);
        Singleton.instance.gameEvent.onPawnStateChange.Invoke(ownerPawn);
    }
}
