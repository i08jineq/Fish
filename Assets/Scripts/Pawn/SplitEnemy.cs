using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : Enemy
{
    [SerializeField]
    Pawn[] splitPawns;

    protected override void Dead()
    {
        foreach (var pawn in splitPawns)
        {
            var spawnedPawn = PawnFactory.CreatePawn(pawn);
            spawnedPawn.transform.position = transform.position + new Vector3(Random.Range(-3,3), 0, Random.Range(-3, 3));
            spawnedPawn.controller = new AiController();
            spawnedPawn.controller.Init(spawnedPawn);
        }
        base.Dead();        
    }
}
