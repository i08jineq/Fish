using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : IController
{
    Pawn targetPawn;
    Pawn playerPawn;

    public void Init(Pawn pawn)
    {
        targetPawn = pawn;
        playerPawn = Singleton.instance.playerPawn;
    }

    public void OnUpdate(float deltaTime)
    {
        targetPawn.FaceTo(playerPawn.transform.position);

        if (Vector3.Distance(targetPawn.transform.position, playerPawn.transform.position) < 10f)
        {
            targetPawn.SpawnAttackObject();
        }
        else
        {
            targetPawn.Move((playerPawn.transform.position - targetPawn.transform.position).normalized * targetPawn.pawnState.Speed);
        }
    }
}
