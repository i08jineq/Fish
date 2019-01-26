using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameMain : MonoBehaviour
{
    [SerializeField] private Pawn playerPawnPrefab;
    [SerializeField] private Pawn nimoPawnPrefab;
    [SerializeField] private List<Stage> stages = new List<Stage>();
    [SerializeField] private FadeLayer fadeLayer;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private TopPanel topPanel;
    private PawnManager pawnManager;
    private AttackObjectManager attackObjectManager;
    private Stage currentStage;
    private int stageIndex = 0;
    private bool isPlaying = false;
    private int totalScore = 0;
    private int nextLevel = 10;
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
        SetupPauseScreen();
        yield return null;
        CreatePlayerPawn();

        CreateCurrentIndexStage(0);

        SetupTopPanel();
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

    private void SetupEvent()
    {
        Singleton.instance.gameEvent.deadEvent.AddListener(OnPawnDie);
        Singleton.instance.gameEvent.onStageCleared.AddListener(OnStageCleared);
        Singleton.instance.gameEvent.onPawnStateChange.AddListener(OnPawnStateChanged);
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

    private void SetupPauseScreen()
    {
        pauseButton.onClick.AddListener(OnClickedPauseButton);

        pauseScreen.Init();
        pauseScreen.onClickedQuit.AddListener(OnClickedQuit);
        pauseScreen.onClickedResume.AddListener(OnClickedResume);
        pauseScreen.onClickedBackTOTitle.AddListener(OnClickedToTitle);
        pauseScreen.gameObject.SetActive(false);
    }

    private void SetupTopPanel()
    {
        topPanel.UpdateHPBar(Singleton.instance.playerPawn.pawnState.Hp);
        topPanel.UpdateScore(totalScore, 0);
    }

    #endregion

    private void OnPawnDie(Pawn pawn)
    {
        if (pawn == Singleton.instance.playerPawn)
        {
            gameOverUI.gameObject.SetActive(true);
            isPlaying = false;
        }
        else
        {
            totalScore += pawn.score;
            topPanel.UpdateScore(totalScore, (float)totalScore / nextLevel);
            if (totalScore >= nextLevel)
            {
                Debug.Log("Level Up pause game, Choice powerup");
            }
        }
    }

    private void OnStageCleared()
    {
        GameObject.Destroy(currentStage.gameObject);
        stageIndex++;
        CreateCurrentIndexStage(stageIndex);
    }

    private void CreateCurrentIndexStage(int max)
    {
        if (max > stages.Count)
        {
            max = stages.Count;
        }

        currentStage = GameObject.Instantiate<Stage>(stages[Random.Range(0, max)]);
        currentStage.Init();
    }

    private void Update()
    {
        if (isPlaying)
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

    private void OnClickedPauseButton()
    {
        pauseScreen.gameObject.SetActive(true);
        isPlaying = false;
    }

    private void OnClickedQuit()
    {
        Application.Quit();
    }

    private void OnClickedResume()
    {
        isPlaying = true;
        pauseScreen.gameObject.SetActive(false);
    }

    private void OnClickedToTitle()
    {
        StartCoroutine(GoBackHomeEnumerator());
    }

    private void OnPawnStateChanged(Pawn pawn)
    {
        Debug.Log("here");
        if (pawn == Singleton.instance.playerPawn)
        {
            topPanel.UpdateHPBar(pawn.pawnState.Hp);
        }
    }
}
