using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
  public void SlowDown()
  {
    StartCoroutine(SlowDownCoroutine());
  }

  public void Accelerate()
  {
    StartCoroutine(AccelerateCoroutine());
  }

  IEnumerator SlowDownCoroutine()
  {
    while (Time.timeScale > 0.2f)
    {
      yield return new WaitForSeconds(0.01f);
      Time.timeScale -= 0.05f;
    }

    Time.timeScale = 0;
  }

  IEnumerator AccelerateCoroutine()
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
