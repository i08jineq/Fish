using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager
{
    private List<Pawn> pawnList = new List<Pawn>();
    private int pawnCount = 0;

    public PawnManager()
    {
        Singleton.instance.gameEvent.onSpawnedPawn.AddListener(OnSpawnedPawn);
        Singleton.instance.gameEvent.deadEvent.AddListener(OnPawnDeath);
    }

    private void OnSpawnedPawn(Pawn pawn)
    {
        pawnList.Add(pawn);
        pawnCount = pawnList.Count;
    }

    private void OnPawnDeath(Pawn pawn)
    {
        pawnList.Remove(pawn);
        pawnCount = pawnList.Count;
    }

    public void OnUpdate(float deltaTime)
    {
        for (int i = 0; i < pawnCount; i++)
        {
            pawnList[i].OnUpdate(deltaTime);
        }

    }
}
