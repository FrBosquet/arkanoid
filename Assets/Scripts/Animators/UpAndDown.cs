using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
  public float acceleration = 0.1f;
  public float maxSpeed = 1;
  public float margin = 1;

  private float maxY;
  private float minY;
  private float speed = 0;
  private bool goingUp = false;

  private void Awake()
  {
    maxY = transform.position.y + margin / 2;
    minY = transform.position.y - margin / 2;
  }

  private void Update()
  {
    if (goingUp && speed < maxSpeed)
    {
      speed += acceleration;
    }
    else if (!goingUp && speed > -maxSpeed)
    {
      speed -= acceleration;
    }

    transform.position += Vector3.up * speed * Time.deltaTime;

    if (goingUp && transform.position.y > maxY)
    {
      goingUp = false;
    }
    else if (!goingUp && transform.position.y < minY)
    {
      goingUp = true;
    }
  }
}
