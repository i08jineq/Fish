using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PawnFactory
{
    public static Pawn CreatePawn(SpawnData spawnData)
    {
        Pawn pawn = CreatePawn(spawnData.Pawn);
        pawn.transform.position = spawnData.spawnPosition;
        return pawn;
    }

    public static Pawn CreatePawn(Pawn pawnPrefab)
    {
        Pawn pawn = GameObject.Instantiate<Pawn>(pawnPrefab);
        pawn.Init();
        Singleton.instance.gameEvent.onSpawnedPawn.Invoke(pawn);
        return pawn;
    }
}
