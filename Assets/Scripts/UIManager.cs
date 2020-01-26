using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSorter : IComparer<Score>
{
  public int Compare(Score x, Score y)
  {
    return y.Points - x.Points;
  }
}

public class UIManager : MonoBehaviour
{
  public List<GameObject> screens;
  public string playerName = "NONAME";
  public GameObject HighScorePrefab;
  public int maxHighScores = 8;

  public void Hide()
  {
    foreach (GameObject screen in screens)
    {
      screen.SetActive(false);
    }
  }

  public void Show(string name)
  {
    foreach (GameObject screen in screens)
    {
      screen.SetActive(screen.name == name);
    }
  }

  public void SetEndgameText(string text)
  {
    TextMeshProUGUI endGameText = GameObject.Find("EndGameText").GetComponent<TextMeshProUGUI>();
    endGameText.text = text;
  }

  public void SetPlayerName(string text)
  {
    playerName = text;
  }

  public void UpdateHighScores(List<Score> scores)
  {
    scores.Sort(new ScoreSorter());

    GameObject highScores = GameObject.Find("HighScores");

    foreach (Transform child in highScores.transform)
    {
      GameObject.Destroy(child.gameObject);
    }

    int listLenght = scores.Count < maxHighScores ? scores.Count : maxHighScores;

    for (int i = 0; i < listLenght; i++)
    {
      GameObject newScore = Instantiate(HighScorePrefab, highScores.transform.position + Vector3.down * 35f * i, Quaternion.identity);
      newScore.transform.SetParent(highScores.transform);
      TextMeshProUGUI[] childs = newScore.GetComponentsInChildren<TextMeshProUGUI>();
      childs[0].text = scores[i].Points.ToString();
      childs[1].text = scores[i].Name;
    }
  }

  public void UpdateScoreResume(int score)
  {
    TextMeshProUGUI puntuationResume = GameObject.Find("PuntuationResume").GetComponent<TextMeshProUGUI>();
    puntuationResume.text = "You achieved " + score.ToString() + " points!";
  }
}
