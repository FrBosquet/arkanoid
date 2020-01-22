using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private new Rigidbody rigidbody;
  public float moveSpeed = 50;
  public GameObject paddle;
  public GameObject ballPrefab;
  public GameObject ball;
  private bool dead;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (dead) return;

    float horizontalInput = Input.GetAxis("Horizontal");
    bool shoot = Input.GetButtonDown("Shoot");

    rigidbody.AddForce(moveSpeed * Vector3.right * Time.deltaTime * horizontalInput, ForceMode.Force);

    if (ball != null)
    {
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

  public void Restart()
  {
    dead = false;
    transform.position = new Vector3(0, -5, 0);
    ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
  }

  public void SetDead(bool isDead)
  {
    dead = isDead;
  }
}
