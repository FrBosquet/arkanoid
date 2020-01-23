using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePowerUp : PowerUp
{
  protected override void Effect()
  {
    Ball[] balls = FindObjectsOfType<Ball>();

    foreach (Ball ball in balls)
    {
      ball.Split();
    }

  }
}
