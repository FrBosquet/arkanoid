using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePowerUp : PowerUp
{
  protected override void Effect()
  {
    player.Grow();
  }
}
