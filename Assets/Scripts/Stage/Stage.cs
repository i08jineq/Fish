using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<SpawnData> spawnDatas = new List<SpawnData>();
    private float spawnTimeCount = 0;
    private bool isSpawnedAllPawn = false;

    private List<Pawn> spawnedPawn = new List<Pawn>();

    public void Init()
    {
        Singleton.instance.gameEvent.onSpawnedPawn.AddListener(OnSpawnedPawn);
        Singleton.instance.gameEvent.deadEvent.AddListener(OnPawnDeath);
    }

    #region event
    //
    public void OnUpdate(float deltaTime)
    {
        spawnTimeCount += deltaTime;
        TrySpawnMonster();
    }

    private void TrySpawnMonster()
    {
        if (isSpawnedAllPawn)
        {
            return;
        }

        int count = spawnDatas.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            if (spawnDatas[i].spawnTime < spawnTimeCount)
            {
                Pawn pawn = PawnFactory.CreatePawn(spawnDatas[i]);
                spawnedPawn.Add(pawn);
                pawn.controller = new AiController();
                pawn.controller.Init(pawn);

                spawnDatas.RemoveAt(i);
                if (spawnDatas.Count <= 0)
                {
                    isSpawnedAllPawn = true;
                }
            }
        }
    }

    private void OnSpawnedPawn(Pawn pawn)
    {
        spawnedPawn.Add(pawn);
    }

    private void OnPawnDeath(Pawn pawn)
    {
        if (spawnedPawn.Remove(pawn))
        {
            if (isSpawnedAllPawn && spawnedPawn.Count <= 0)
            {
                Singleton.instance.gameEvent.onStageCleared.Invoke();
            }
        }
    }

    #endregion

    private void OnDestroy()
    {
        Singleton.instance.gameEvent.onSpawnedPawn.RemoveListener(OnSpawnedPawn);
        Singleton.instance.gameEvent.deadEvent.RemoveListener(OnPawnDeath);
    }

    private void OnDrawGizmos()
    {
        int count = spawnDatas.Count;
        Gizmos.color = Color.red;
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(transform.position + spawnDatas[i].spawnPosition, 1);
        }
    }
}
