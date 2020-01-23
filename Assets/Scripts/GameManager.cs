using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public List<GameObject> levels;
  public GameObject gameOverScreen;
  public GameObject pauseScreen;
  public GameObject victoryScreen;
  public GameObject lifeTokenPrefab;
  public TextMeshPro scoreBoard;

  [SerializeField] private int lifes;
  private Player playerScript;
  private LevelManager levelManager;
  private int score;
  [SerializeField] private int bricksLeft = 0;

  List<GameObject> lifeTokens = new List<GameObject>();

  private bool pause = false;
  private bool victory = false;

  private void Awake()
  {
    playerScript = GameObject.Find("Player").GetComponent<Player>();
    levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
    gameOverScreen.SetActive(false);
    victoryScreen.SetActive(false);
    lifes = 2;
    bricksLeft = levelManager.ResetProgresion();
    playerScript.Restart();
    Time.timeScale = 1;
    score = 0;

    UpdateScore();

    for (int i = 0; i < lifes; i++)
    {
      Transform playerTransform = playerScript.transform;
      Vector3 farPosition = playerTransform.position + Vector3.right * 0.55f;
      Vector3 offset = i * 0.15f * Vector3.left;

      GameObject token = Instantiate(lifeTokenPrefab, farPosition + offset, lifeTokenPrefab.transform.rotation);
      token.transform.SetParent(playerTransform);
      lifeTokens.Add(token);
    }
  }

  public void LoseBall()
  {
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
      StartCoroutine(SlowDown());
    }
    else
    {
      pauseScreen.SetActive(false);
      StartCoroutine(Accelerate());
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
    StartCoroutine(SlowDown());
    victory = true;
    victoryScreen.SetActive(true);
  }

  public void NextLevel()
  {
    foreach (Ball ball in FindObjectsOfType<Ball>())
    {
      Destroy(ball.gameObject);
    }

    bricksLeft = levelManager.AdvanceToNextLevel();
    victory = false;
    victoryScreen.SetActive(false);
    playerScript.Restart();
    StartCoroutine(Accelerate());
  }

  public void AddPoints(int points)
  {
    score += points;
    UpdateScore();
  }

  private void UpdateScore()
  {
    scoreBoard.text = score.ToString();
  }

  IEnumerator SlowDown()
  {
    while (Time.timeScale > 0.2f)
    {
      yield return new WaitForSeconds(0.01f);
      Time.timeScale -= 0.05f;
    }

    Time.timeScale = 0;
  }

  IEnumerator Accelerate()
  {
    Time.timeScale = 0.2f;
    while (Time.timeScale < 0.98)
    {
      yield return new WaitForSeconds(0.01f);
      Time.timeScale += 0.05f;
    }

    Time.timeScale = 1;
  }
}
