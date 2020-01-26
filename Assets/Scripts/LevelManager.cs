using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public List<GameObject> levels;
  public bool isLastLevel = false;

  private GameObject level;
  private int currentLevelIndex = 0;

  private void Start()
  {
    UpdateIsLastLevel();
  }

  private int LoadLevel(int index)
  {
    Clear();
    level = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
    level.transform.SetParent(transform);

    return gameObject.GetComponentsInChildren<Brick>().Length;
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
    UpdateIsLastLevel();

    return LoadLevel();
  }

  private void UpdateIsLastLevel()
  {
    isLastLevel = levels.Count == currentLevelIndex + 1;
  }

  public void Clear()
  {
    Destroy(level);
  }
}
