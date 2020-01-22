using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject gameOverScreen;
  public GameObject pauseScreen;
  public List<GameObject> levels;
  private GameObject level;
  private Player playerScript;
  private int levelIndex = 0;
  [SerializeField] private int lifes;

  private void Awake()
  {
    playerScript = GameObject.Find("Player").GetComponent<Player>();
  }

  private void Start()
  {
    NewGame();
  }

  public void NewGame()
  {
    gameOverScreen.SetActive(false);
    levelIndex = 0;
    lifes = 2;
    LoadLevel(0);
    playerScript.Restart();
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

  private void LoadLevel(int index)
  {
    Destroy(level);
    level = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
  }
}
