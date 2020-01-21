using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private new Rigidbody rigidbody;
  public float moveSpeed = 50;
  public GameObject gameOverScreen;
  public GameObject pauseScreen;
  public GameObject paddle;
  public GameObject ballPrefab;
  public GameObject ball;
  public int lifesLeft;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    Restart();
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

  public void Loose()
  {
    if (lifesLeft == 0)
    {
      gameOverScreen.SetActive(true);
    }
    else
    {
      lifesLeft--;
      ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
  }

  public void Restart()
  {
    gameOverScreen.SetActive(false);
    lifesLeft = 2;
    transform.position = new Vector3(0, -5, 0);
    ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
  }
}
