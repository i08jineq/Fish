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
    [SerializeField] private CameraEffect cameraEffect;
    [SerializeField] private float shakeCameraStrength = 0.5f;
    [SerializeField] private float shakeCameraPeriod = 0.5f;
    [SerializeField] private FlockGroup flockGroup;
    [SerializeField] private DamageText damageText;
    [SerializeField] private float damageTextLifeTime = 1;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip powerupSound;
    [SerializeField] private AudioClip levelupSound;
    [SerializeField] private RewardUI rewardUI;
    [SerializeField] private List<PowerUp> powerups = new List<PowerUp>();
    private PowerUpComponent playerPowerupComponent;
    private SoundManager soundManager;
    private PawnManager pawnManager;
    private AttackObjectManager attackObjectManager;
    private Stage currentStage;
    private int stageIndex = 0;
    private bool isPlaying = false;
    private int totalScore = 0;
    private int nextLevel = 10;
    private int currentLevel = 1;

    private const float positionMinZ = -7;
    private const float positionMaxZ = 10;
    private const float positionXRange = 20;
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
        SetupCameraEffect();
        yield return null;
        CreatePlayerPawn();

        CreateCurrentIndexStage(0);

        SetupTopPanel();
        SetupSound();
        yield return null;

        SetupPowerup();

        yield return null;

        SetupButtonSound();
        SetupFlockGroup();

        yield return null;

        yield return fadeLayer.FadeInEnumerator(2);
        isPlaying = true;
        OnStartGame();
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
        Singleton.instance.gameEvent.takeDamageEvent.AddListener(OnPawnTakeDamage);
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
        pauseButton.gameObject.SetActive(false);

        pauseScreen.Init();
        pauseScreen.onClickedQuit.AddListener(OnClickedQuit);
        pauseScreen.onClickedResume.AddListener(OnClickedResume);
        pauseScreen.onClickedBackTOTitle.AddListener(OnClickedToTitle);
        pauseScreen.gameObject.SetActive(false);

    }

    private void SetupPowerup()
    {
        playerPowerupComponent = new PowerUpComponent();
        playerPowerupComponent.Init(Singleton.instance.playerPawn);
        rewardUI.Init();
        rewardUI.onSelected.AddListener(OnSelectedReward);
        rewardUI.gameObject.SetActive(false);
    }

    private void SetupSound()
    {
        soundManager = new SoundManager();
    }

    private void SetupCameraEffect()
    {
        cameraEffect.Init();
    }

    private void SetupTopPanel()
    {
        topPanel.UpdateHPBar(Singleton.instance.playerPawn.pawnState.Hp);
        topPanel.UpdateScore(totalScore, 0);
    }

    private void OnStartGame()
    {
        pauseButton.gameObject.SetActive(true);
        Singleton.instance.gameEvent.onPlayBGM.Invoke(bgm);
    }

    private void SetupFlockGroup()
    {
        flockGroup.Init(Singleton.instance.playerPawn.transform.position);
    }

    private void SetupButtonSound()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (var item in buttons)
        {
            HoverComponent hover = item.gameObject.AddComponent<HoverComponent>();
            hover.onHovered.AddListener(PlayHoverSound);
            item.onClick.AddListener(PlayClickSound);
        }
    }
    #endregion

    private void OnPawnDie(Pawn pawn)
    {
        Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(deathSound);
        if (pawn == Singleton.instance.playerPawn)
        {
            gameOverUI.gameObject.SetActive(true);
            isPlaying = false;
        }
        else
        {
            totalScore += pawn.score;

            if (totalScore >= nextLevel)
            {
                isPlaying = false;
                OpenRewardScreen();
                LevelUp();
            }

            topPanel.UpdateScore(totalScore, (float)totalScore / nextLevel);
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
        int randomIndex = Mathf.Min(max, stages.Count);
        currentStage = GameObject.Instantiate<Stage>(stages[Random.Range(0, randomIndex)]);
        currentStage.Init(max);
    }

    private void Update()
    {
        if (isPlaying)
        {
            float deltaTime = Time.deltaTime;
            pawnManager.OnUpdate(deltaTime);
            currentStage.OnUpdate(deltaTime);
            attackObjectManager.OnUpdate(deltaTime);
            ClampPlayerPosition();
            UpdatePowerup(deltaTime);

            flockGroup.UpdatePosition(Singleton.instance.playerPawn.transform.position);
            flockGroup.OnUpdate(deltaTime);
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
        if (pawn == Singleton.instance.playerPawn)
        {
            topPanel.UpdateHPBar(pawn.pawnState.Hp);
        }
    }

    private void ClampPlayerPosition()
    {
        Vector3 pos = Singleton.instance.playerPawn.transform.position;
        pos.z = Mathf.Clamp(pos.z, positionMinZ, positionMaxZ);
        pos.x = Mathf.Clamp(pos.x, -positionXRange, positionXRange);
        Singleton.instance.playerPawn.transform.position = pos;
    }

    private void OnPawnTakeDamage(Pawn pawn, float damage)
    {
        DamageText dmgText= GameObject.Instantiate<DamageText>(damageText);
        dmgText.SetText((int)damage);
        dmgText.transform.position = pawn.transform.position;
        GameObject.Destroy(dmgText.gameObject, damageTextLifeTime);

        if(pawn == Singleton.instance.playerPawn)
        {
            cameraEffect.ShakeCamera(shakeCameraStrength, shakeCameraPeriod);
        }
        else
        {
            cameraEffect.ShakeCamera(shakeCameraStrength/2, shakeCameraPeriod/2);
        }
    }

    private void PlayHoverSound()
    {
        Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(hoverSound);
    }

    private void PlayClickSound()
    {
        Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(clickSound);
    }

    private void OpenRewardScreen()
    {
        int number = rewardUI.buttons.Count;
        List<PowerUp> allPowerUp = new List<PowerUp>();
        allPowerUp.AddRange(powerups);
        List<PowerUp> targetPowerup = new List<PowerUp>();
        for (int i = 0; i < number; i++)
        {
            int targetIndex = Random.Range(0, allPowerUp.Count);
            targetPowerup.Add(allPowerUp[targetIndex]);
            allPowerUp.RemoveAt(targetIndex);
        }

        rewardUI.SetRewards(targetPowerup.ToArray());
        rewardUI.gameObject.SetActive(true);
        Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(levelupSound);
    }

    private void UpdatePowerup(float deltaTime)
    {
        playerPowerupComponent.OnUpdate(deltaTime);
    }

    private void LevelUp()
    {
        currentLevel++;
        nextLevel = currentLevel * 5 + 10;
    }

    private void OnSelectedReward(PowerUp powerup)
    {
        rewardUI.gameObject.SetActive(false);
        isPlaying = true;
        playerPowerupComponent.AddPowerUp(powerup);
        Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(powerupSound);

    }
}
