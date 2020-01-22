using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public List<GameObject> levels;
  private GameObject level;
  private int levelIndex = 0;

  private void Start()
  {
    Restart();
  }

  public void Restart()
  {
    levelIndex = 0;
    LoadLevel(0);
  }

  private void LoadLevel(int index)
  {
    Destroy(level);
    level = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
  }
}
