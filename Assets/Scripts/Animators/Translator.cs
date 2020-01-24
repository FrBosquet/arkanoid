using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
  public float speed = 1;

  private void Update()
  {
    if (transform.position.x > 8)
    {
      transform.position = transform.position + Vector3.left * 16;
    }
    else if (transform.position.x < -8f)
    {
      transform.position = transform.position + Vector3.right * 16;

    }

    transform.position += Vector3.right * speed * Time.deltaTime;
  }
}
