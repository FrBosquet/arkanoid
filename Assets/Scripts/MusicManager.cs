using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  public float minimumPitch = 0.25f;
  public float maximumPitch = 1f;
  public float minimumCutoff = 10f;
  public float maximuCutoff = 5000f;
  public bool slowDown = false;

  private AudioSource audioSource;
  private AudioLowPassFilter filter;

  private void Awake()
  {
    audioSource = gameObject.GetComponent<AudioSource>();
    filter = gameObject.GetComponent<AudioLowPassFilter>();
  }

  public void SlowDown()
  {
    slowDown = true;
  }

  public void Accelerate()
  {
    slowDown = false;
  }

  private void Update()
  {
    if (slowDown)
    {
      if (audioSource.pitch > minimumPitch + 0.1f)
      {
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, minimumPitch, 0.05f);
      }
      else
      {
        audioSource.pitch = minimumPitch;

      }

      if (filter.cutoffFrequency > minimumCutoff + 0.1f)
      {
        filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, minimumCutoff, 0.05f);
      }
      else
      {
        filter.cutoffFrequency = minimumCutoff;

      }
    }
    else
    {
      if (audioSource.pitch < maximumPitch - 0.1f)
      {
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, maximumPitch, 0.05f);
      }
      else
      {
        audioSource.pitch = maximumPitch;
      }

      if (filter.cutoffFrequency < maximuCutoff + 0.1f)
      {
        filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, maximuCutoff, 0.05f);
      }
      else
      {
        filter.cutoffFrequency = maximuCutoff;

      }
    }
  }

  public void Restart()
  {
    audioSource.time = 0;
  }
}
