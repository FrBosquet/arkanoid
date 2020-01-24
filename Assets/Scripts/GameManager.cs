using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public List<GameObject> levels;
  public GameObject gameOverScreen;
  public GameObject pauseScreen;
  public GameObject victoryScreen;
  public GameObject lifeTokenPrefab;

  [SerializeField] private int lifes;
  private Player playerScript;
  private LevelManager levelManager;
  private ScoreManager scoreManager;
  private TimeManager timeManager;
  [SerializeField] private int bricksLeft = 0;

  List<GameObject> lifeTokens = new List<GameObject>();

  private bool pause = false;
  private bool victory = false;

  private void Awake()
  {
    playerScript = GameObject.Find("Player").GetComponent<Player>();
    levelManager = GetComponent<LevelManager>();
    scoreManager = GetComponent<ScoreManager>();
    timeManager = GetComponent<TimeManager>();
  }

  private void Start()
  {
    NewGame();
  }

  private void Update()
  {
    if (Input.GetButtonDown("Pause"))
    {
      TogglePause();
    }
  }

  public void NewGame()
  {
    Clean();
    gameOverScreen.SetActive(false);
    victoryScreen.SetActive(false);
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
      gameOverScreen.SetActive(true);
      playerScript.SetDead(true);
    }
    else
    {
      GameObject lastLifeToken = lifeTokens[lifeTokens.Count - 1];
      lifeTokens.Remove(lastLifeToken);
      Destroy(lastLifeToken);
      playerScript.Restart();
    }
  }

  public void TogglePause()
  {
    if (victory) return;

    pause = !pause;

    if (pause)
    {
      pauseScreen.SetActive(true);
      timeManager.SlowDown();
    }
    else
    {
      pauseScreen.SetActive(false);
      timeManager.Accelerate();
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

  public void ShowVictory()
  {
    timeManager.SlowDown();
    victory = true;
    victoryScreen.SetActive(true);
  }

  public void NextLevel()
  {
    Clean();

    bricksLeft = levelManager.AdvanceToNextLevel();
    victory = false;
    victoryScreen.SetActive(false);
    playerScript.Restart();
    timeManager.Accelerate();
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
  }

}
