using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI 연결")]
    public Text scoreText;
    public Text timerText;
    public GameObject gameOverPanel; // 게임오버 창 오브젝트
    public Text gameOverReasonText;  // 게임오버 사유 적을 텍스트

    [Header("게임 설정")]
    public float timeLimit = 60f;     // 플레이 타임 1분 내외 요구사항
    public float fallThreshold = -10f; // 이 Y값 아래로 떨어지면 추락 처리

    private int currentScore = 0;
    private float timeRemaining;
    private bool isGameFinished = false;
    private Transform playerTransform;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        timeRemaining = timeLimit;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateScoreUI();

        // 씬에서 플레이어를 찾아 추락 감지용 위치 기록
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        if (isGameFinished) return;

        // 1. 타이머 기능 (필수 요구사항 3)
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            GameOver("시간 초과!");
        }

        // 2. 추락 감지 기능 (필수 요구사항 3)
        if (playerTransform != null && playerTransform.position.y < fallThreshold)
        {
            GameOver("추락했습니다!");
        }
    }

    public void AddScore(int amount)
    {
        if (isGameFinished) return;
        currentScore += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
    }

    void UpdateTimerUI()
    {
        if (timerText != null) timerText.text = "Time: " + Mathf.Max(0, timeRemaining).ToString("F1") + "s";
    }

    // 게임 오버 처리 (필수 요구사항 3)
    public void GameOver(string reason)
    {
        isGameFinished = true;
        if (gameOverReasonText != null) gameOverReasonText.text = reason;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    // 스테이지 클리어 처리 (가산점 요소: 스테이지 클리어 연출용)
    public void StageClear()
    {
        isGameFinished = true;
        if (gameOverReasonText != null) gameOverReasonText.text = "STAGE CLEAR!";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    // 다시 시작 기능 (필수 요구사항 3 - UI 버튼에 연결)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}