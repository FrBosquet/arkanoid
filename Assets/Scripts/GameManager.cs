using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public List<Score> highScores;

  public GameObject lifeTokenPrefab;

  public TextMeshPro endGameText;

  [SerializeField] private int lifes;
  private Player playerScript;
  private LevelManager levelManager;
  private ScoreManager scoreManager;
  private TimeManager timeManager;
  private MusicManager musicManager;
  private UIManager uiManager;
  [SerializeField] private int bricksLeft = 0;

  List<GameObject> lifeTokens = new List<GameObject>();

  private bool playing = false;
  private bool pause = false;
  private bool victory = false;

  private void Awake()
  {
    playerScript = GameObject.Find("Player").GetComponent<Player>();
    levelManager = GetComponent<LevelManager>();
    scoreManager = GetComponent<ScoreManager>();
    timeManager = GetComponent<TimeManager>();
    musicManager = GetComponent<MusicManager>();
    uiManager = GetComponent<UIManager>();
  }

  private void Start()
  {
    StartScreen();

  }

  private void Update()
  {
    if (Input.GetButtonDown("Pause"))
    {
      TogglePause();
    }
  }

  public void StartScreen()
  {
    Clean();
    highScores = DataManager.GetSaveData();
    uiManager.Show("StartScreen");
    uiManager.UpdateHighScores(highScores);
    scoreManager.Reset();
    playerScript.Restart();
    musicManager.Restart();
    musicManager.Accelerate();
    playing = false;
  }

  public void RegisterPuntuation()
  {
    Score newScore = new Score(scoreManager.score, uiManager.playerName);
    highScores.Add(newScore);
    DataManager.SaveData(highScores);
    levelManager.Clear();
    StartScreen();
  }

  public void NewGame()
  {
    playing = true;

    uiManager.Hide();

    lifes = 0;
    bricksLeft = levelManager.ResetProgresion();
    playerScript.Restart();
    Time.timeScale = 1;
    scoreManager.Reset();

    for (int i = 0; i < 2; i++) AddLife();
  }

  public void LoseBall()
  {
    Ball[] balls = FindObjectsOfType<Ball>();

    if (balls.Length > 1) return;

    lifes--;

    if (lifes < 0)
    {
      EndGame("Game Over");
    }
    else
    {
      GameObject lastLifeToken = lifeTokens[lifeTokens.Count - 1];
      lifeTokens.Remove(lastLifeToken);
      Destroy(lastLifeToken);
      playerScript.Restart();
    }
  }

  public void EndGame(string message)
  {
    playing = false;

    uiManager.Show("GameOverScreen");
    uiManager.SetEndgameText(message);
    uiManager.UpdateScoreResume(scoreManager.score);

    timeManager.SlowDown();

    playerScript.SetDead(true);
  }

  public void AddPoints(int points)
  {
    scoreManager.AddPoints(points);
  }

  public void AddLife()
  {
    Transform playerTransform = playerScript.transform;
    Vector3 farPosition = playerTransform.position + Vector3.right * 0.55f;
    Vector3 offset = lifes * 0.15f * Vector3.left;
    lifes++;

    GameObject token = Instantiate(lifeTokenPrefab, farPosition + offset, lifeTokenPrefab.transform.rotation);
    token.transform.SetParent(playerTransform);
    lifeTokens.Add(token);
  }

  private void Clean()
  {
    foreach (Ball ball in FindObjectsOfType<Ball>())
    {
      Destroy(ball.gameObject);
    }

    foreach (PowerUp powerup in FindObjectsOfType<PowerUp>())
    {
      Destroy(powerup.gameObject);
    }

    foreach (ParticleSystem particle in FindObjectsOfType<ParticleSystem>())
    {
      Destroy(particle.gameObject);
    }
  }

  public void DestroyBrick()
  {
    bricksLeft--;

    if (bricksLeft == 0)
    {
      ShowVictory();
    }
  }

  public void NextLevel()
  {
    Clean();

    bricksLeft = levelManager.AdvanceToNextLevel();
    victory = false;
    uiManager.Hide();
    playerScript.Restart();
    timeManager.Accelerate();
    musicManager.Accelerate();
  }

  public void ShowVictory()
  {
    timeManager.SlowDown();
    musicManager.SlowDown();
    victory = true;

    if (levelManager.isLastLevel)
    {
      uiManager.Show("GameOverScreen");
      uiManager.SetEndgameText("Victory");
    }
    else
    {
      uiManager.Show("VictoryScreen");

    }
  }

  public void TogglePause()
  {
    if (victory) return;
    if (!playing) return;

    pause = !pause;

    if (pause)
    {
      uiManager.Show("PauseScreen");

      timeManager.SlowDown();
      musicManager.SlowDown();
    }
    else
    {
      uiManager.Hide();
      timeManager.Accelerate();
      musicManager.Accelerate();
    }
  }

}
