using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public List<GameObject> levels;
  private GameObject level;
  private int currentLevelIndex = 0;

  private void LoadLevel(int index)
  {
    Destroy(level);
    level = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
    level.transform.SetParent(transform);
  }

  private void LoadLevel()
  {
    LoadLevel(currentLevelIndex);
  }

  public void ResetProgresion()
  {
    currentLevelIndex = 0;
    LoadLevel();
  }

  public void AdvanceToNextLevel()
  {
    currentLevelIndex++;
  }
}
