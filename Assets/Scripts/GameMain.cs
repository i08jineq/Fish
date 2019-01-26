using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] private Pawn playerPawnPrefab;
    [SerializeField] private List<Stage> stages = new List<Stage>();
    private PawnManager pawnManager;
    private Stage currentStage;
    private int stageIndex = 0;

    private bool isPlaying = false;

    #region init

    IEnumerator Start()
    {
        isPlaying = false;

        yield return Singleton.Init();

        SetupPawnManager();

        SetupEvent();
        yield return null;
        CreatePlayerPawn();
        isPlaying = true;
    }

    private void CreatePlayerPawn()
    {
        Singleton.instance.playerPawn = PawnFactory.CreatePawn(playerPawnPrefab);
        Singleton.instance.playerPawn.controller = new PlayerPawnController();
        Singleton.instance.playerPawn.controller.Init(Singleton.instance.playerPawn);

    }

    private void SetupEvent()
    {
        Singleton.instance.gameEvent.deadEvent.AddListener(OnPlayerDie);
        Singleton.instance.gameEvent.onStageCleared.AddListener(OnStageCleared);
    }
    private void SetupPawnManager()
    {
        pawnManager = new PawnManager();
    }

    #endregion

    private void OnPlayerDie(Pawn pawn)
    {
        if (pawn == Singleton.instance.playerPawn)
        {
            Debug.Log("Game Over");
        }
    }

    private void OnStageCleared()
    {

    }

    private void Update()
    {
        if(isPlaying)
        {
            float deltaTime = Time.deltaTime;
            pawnManager.OnUpdate(deltaTime);
        }

    }
}
