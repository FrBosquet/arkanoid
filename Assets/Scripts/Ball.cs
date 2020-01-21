using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  private new Rigidbody rigidbody;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    rigidbody.AddForce(4, 4, 0, ForceMode.VelocityChange);
  }

  void Update()
  {
  }
}
