using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMain : MonoBehaviour
{
    [SerializeField] private Pawn playerPawnPrefab;
    [SerializeField] private Pawn nimoPawnPrefab;
    [SerializeField] private List<Stage> stages = new List<Stage>();
    [SerializeField] private FadeLayer fadeLayer;
    [SerializeField] private GameOverUI gameOverUI;
    private PawnManager pawnManager;
    private AttackObjectManager attackObjectManager;
    private Stage currentStage;
    private int stageIndex = 0;
    private bool isPlaying = false;

    #region init
    private void Awake()
    {
        fadeLayer.ForceColor(Color.black);
    }

    IEnumerator Start()
    {
        isPlaying = false;
        yield return Singleton.Init();
        SetupPawnManager();
        SetupEvent();
        SetupAttackObjectManager();
        SetupGameOverUI();
        yield return null;
        CreatePlayerPawn();
        CreateNimoPawn();

        CreateCurrentIndexStage(0);
        yield return null;
        
        yield return fadeLayer.FadeInEnumerator(2);
        isPlaying = true;
    }

    private void CreatePlayerPawn()
    {
        Singleton.instance.playerPawn = PawnFactory.CreatePawn(playerPawnPrefab);
        Singleton.instance.playerPawn.controller = new PlayerPawnController();
        Singleton.instance.playerPawn.controller.Init(Singleton.instance.playerPawn);

    }

    private void CreateNimoPawn()
    {
        Singleton.instance.nimoPawn = PawnFactory.CreatePawn(nimoPawnPrefab);
        Singleton.instance.nimoPawn.controller = new EmptyController();
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

    private void SetupGameOverUI()
    {
        gameOverUI.gameObject.SetActive(false);
        gameOverUI.onClicked.AddListener(OnClickedGameOver);

    }
    #endregion

    private void OnPlayerDie(Pawn pawn)
    {
        if (pawn == Singleton.instance.nimoPawn)
        {
            gameOverUI.gameObject.SetActive(true);
            isPlaying = false;
        }
    }

    private void OnStageCleared()
    {
        GameObject.Destroy(currentStage.gameObject);
        stageIndex ++; 
        CreateCurrentIndexStage(stageIndex);
    }

    private void CreateCurrentIndexStage(int max)
    {
        if(max > stages.Count)
        {
            max = stages.Count;
        }

        currentStage = GameObject.Instantiate<Stage>(stages[Random.Range(0,max)]);
        currentStage.Init();
    }

    private void Update()
    {
        if(isPlaying)
        {
            float deltaTime = Time.deltaTime;
            pawnManager.OnUpdate(deltaTime);
            currentStage.OnUpdate(deltaTime);
            attackObjectManager.OnUpdate(deltaTime);
        }
    }

    private void OnClickedGameOver()
    {
        StartCoroutine(GoBackHomeEnumerator());
    }

    private IEnumerator GoBackHomeEnumerator()
    {
        yield return fadeLayer.FadeOutEnumerator(Color.black, 2);
        SceneManager.LoadScene("Title");
    }
}
