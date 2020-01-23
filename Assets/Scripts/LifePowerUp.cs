using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUp : PowerUp
{
  protected override void Effect()
  {
    gameManager.AddLife();
  }
}
