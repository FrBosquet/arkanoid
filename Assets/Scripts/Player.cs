using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private new Rigidbody rigidbody;
  public float moveSpeed = 50;
  public GameObject paddle;
  public GameObject ball;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void Update()
  {
    float horizontalInput = Input.GetAxis("Horizontal");

    rigidbody.AddForce(moveSpeed * Vector3.right * Time.deltaTime * horizontalInput, ForceMode.Force);

    if (ball != null)
    {
      bool shoot = Input.GetButtonDown("Shoot");
      ball.transform.position = transform.position + Vector3.up * 0.3f;

      if (shoot)
      {
        ball.GetComponent<Ball>().Shoot();
        ball = null;
      }
    }
  }

  private void LateUpdate()
  {
    paddle.transform.position = transform.position;
  }
}
