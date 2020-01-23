using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public List<GameObject> levels;
  private GameObject level;
  private int currentLevelIndex = 0;

  private int LoadLevel(int index)
  {
    Destroy(level);
    level = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
    level.transform.SetParent(transform);

    return level.transform.childCount;
  }

  private int LoadLevel()
  {
    return LoadLevel(currentLevelIndex);
  }

  public int ResetProgresion()
  {
    currentLevelIndex = 0;
    return LoadLevel();
  }

  public int AdvanceToNextLevel()
  {
    currentLevelIndex++;
    return LoadLevel();
  }
}
