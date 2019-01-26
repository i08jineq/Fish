using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] private Pawn playerPawnPrefab;
    [SerializeField] private List<Stage> stages = new List<Stage>();
    private PawnManager pawnManager = new PawnManager();
    private Stage currentStage;
    private int stageIndex = 0;

    private bool isPlaying = false;

    #region init

    IEnumerator Start()
    {
        Singleton.Init();

        CreatePlayerPawn();
        SetupEvent();
        yield return null;

        isPlaying = true;
    }

    private void CreatePlayerPawn()
    {
        Singleton.instance.playerPawn = GameObject.Instantiate<Pawn>(playerPawnPrefab);
        Singleton.instance.playerPawn.Init();
        Singleton.instance.playerPawn.controller = new PlayerPawnController();

    }

    private void SetupEvent()
    {
        Singleton.instance.gameEvent.deadEvent.AddListener(OnPlayerDie);
        Singleton.instance.gameEvent.onStageCleared.AddListener(OnStageCleared);
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
        float deltaTime = Time.deltaTime;
        pawnManager.OnUpdate(deltaTime);
    }
}
