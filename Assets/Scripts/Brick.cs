using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
  [SerializeField] private int life = 1;

  public void Hit()
  {
    life--;

    if (life == 0)
    {
      Destroy(gameObject);
    }
  }
}
