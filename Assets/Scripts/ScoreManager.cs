using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
  private TextMeshPro scoreBoard;
  public int score;

  private void Awake()
  {
    scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshPro>();
  }

  public void Reset()
  {
    score = 0;
    UpdateScore();
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
}
