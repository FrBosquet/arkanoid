using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
  public float speed = 60;

  void Update()
  {
    transform.Rotate(transform.up, speed * Time.deltaTime);
  }
}
