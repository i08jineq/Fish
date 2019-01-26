using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : IController
{
    private Pawn targetPawn;
    private Pawn nimoPawn;
    private AIDataComponent aiDataComponent;
    private float attackIntervalCount = 0;

    public void Init(Pawn pawn)
    {
        targetPawn = pawn;
        aiDataComponent = pawn.GetComponent<AIDataComponent>();
        nimoPawn = Singleton.instance.nimoPawn;
    }

    public void OnUpdate(float deltaTime)
    {
        targetPawn.FaceTo(nimoPawn.transform.position);

        if (Vector3.Distance(targetPawn.transform.position, nimoPawn.transform.position) < targetPawn.pawnState.Range)
        {
            TryAttack(deltaTime);
        }
        else
        {
            targetPawn.Move((nimoPawn.transform.position - targetPawn.transform.position).normalized);
        }
    }

    private void TryAttack(float deltaTime)
    {
        aiDataComponent.attackCountTime += deltaTime;
        if(aiDataComponent.attackCountTime > aiDataComponent.attackInterval)
        {
            aiDataComponent.attackCountTime = 0;
            targetPawn.SpawnAttackObject();
        }
    }
}
