using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] private Pawn playerPawnPrefab;
    [SerializeField] private List<Stage> stages = new List<Stage>();
    private PawnManager pawnManager;
    private AttackObjectManager attackObjectManager;
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
        SetupAttackObjectManager();

        yield return null;
        CreatePlayerPawn();

        CreateCurrentIndexStage();
        yield return null;
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

    private void SetupAttackObjectManager()
    {
        attackObjectManager = new AttackObjectManager();
        attackObjectManager.Init();
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

    private void CreateCurrentIndexStage()
    {
        currentStage = GameObject.Instantiate<Stage>(stages[stageIndex]);
        currentStage.Init();
    }

    private void Update()
    {
        if(isPlaying)
        {
            float deltaTime = Time.deltaTime;
            pawnManager.OnUpdate(deltaTime);
            currentStage.OnUpdate(deltaTime);
        }

    }
}
