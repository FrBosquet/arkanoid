using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public List<GameObject> levels;
  public GameObject gameOverScreen;
  public GameObject pauseScreen;

  [SerializeField] private int lifes;
  private Player playerScript;
  private LevelManager levelManager;
  private int levelIndex = 0;

  private bool pause = false;

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
    levelIndex = 0;
    lifes = 2;
    levelManager.ResetProgresion();
    playerScript.Restart();
    Time.timeScale = 1;
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
      playerScript.Restart();
    }
  }

  public void TogglePause()
  {
    pause = !pause;

    if (pause)
    {
      pauseScreen.SetActive(true);
      Time.timeScale = 0;
    }
    else
    {
      pauseScreen.SetActive(false);
      Time.timeScale = 1;
    }
  }
}
