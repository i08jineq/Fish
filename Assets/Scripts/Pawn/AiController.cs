using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : IController
{
    private Pawn targetPawn;
    private Pawn playerPawn;
    private AIDataComponent aiDataComponent;
    private float attackIntervalCount = 0;

    public void Init(Pawn pawn)
    {
        targetPawn = pawn;
        aiDataComponent = pawn.GetComponent<AIDataComponent>();
        aiDataComponent.Init();
        playerPawn = Singleton.instance.playerPawn;
    }

    public void OnUpdate(float deltaTime)
    {
        TryAttack(deltaTime);
        MovePawn();
    }

    private void MovePawn()
    {
        Vector3 diff = playerPawn.transform.position - targetPawn.transform.position;
        float useTurnSpeed = aiDataComponent.isCharging ? targetPawn.pawnState.ChargeTurnSpeed : targetPawn.pawnState.TurnSpeed;
        float useMoveSpeed = aiDataComponent.isCharging ? targetPawn.pawnState.ChargeSpeed : targetPawn.pawnState.Speed;

        if (diff.magnitude > targetPawn.pawnState.Range)
        {
            targetPawn.TurnTo(diff, useTurnSpeed);
        }

        targetPawn.MoveForward(useMoveSpeed);
    }

    private void TryAttack(float deltaTime)
    {
        if(aiDataComponent.isCharging )
        {
            aiDataComponent.chargeTimeCount += deltaTime;
            if(aiDataComponent.chargeTimeCount > aiDataComponent.chargePeriod)
            {
                aiDataComponent.chargeTimeCount = 0;
                aiDataComponent.attackCountTime = 0;
                aiDataComponent.isCharging = false;
            }
            return;
        }
        aiDataComponent.attackCountTime += deltaTime;
        if(aiDataComponent.attackCountTime > aiDataComponent.attackInterval)
        {
            aiDataComponent.isCharging = true;
        }
    }
}
