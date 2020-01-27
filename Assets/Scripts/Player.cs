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
  private bool dead = true;
  private float BALL_HEIGHT = 0.3f;
  private Vector3 DEFAULT_SCALE;
  private Vector3 POWERUP_SCALE = new Vector3(1, 1.2f, 1);

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    DEFAULT_SCALE = transform.localScale;
  }

  void Update()
  {
    if (dead) return;

    float horizontalInput = Input.GetAxis("Horizontal");
    bool shoot = Input.GetButtonDown("Shoot");

    rigidbody.AddForce(moveSpeed * Vector3.right * Time.deltaTime * horizontalInput, ForceMode.Force);

    if (ball != null)
    {
      ball.transform.position = transform.position + Vector3.up * BALL_HEIGHT;

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
    ball = Instantiate(ballPrefab, transform.position + Vector3.up * BALL_HEIGHT, Quaternion.identity);
    ball.GetComponent<Ball>().flying = false;
    ResetScale();
  }

  public void SetDead(bool isDead)
  {
    dead = isDead;
  }

  public void Grow()
  {
    transform.localScale = Vector3.Scale(transform.localScale, POWERUP_SCALE);
    paddle.transform.localScale = Vector3.Scale(transform.localScale, POWERUP_SCALE);
  }

  public void ResetScale()
  {
    transform.localScale = DEFAULT_SCALE;
    paddle.transform.localScale = DEFAULT_SCALE;
  }
}
