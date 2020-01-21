using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  public float paddleEffectAmmount;
  private new Rigidbody rigidbody;
  private AudioSource kickSound;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    kickSound = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("GameEnd");
    Destroy(gameObject);
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("Brick"))
    {
      Brick brick = other.gameObject.GetComponent<Brick>();
      brick.Hit();
    }
    else if (other.gameObject.CompareTag("Wall"))
    {
      kickSound.Play();
    }
    else if (other.gameObject.CompareTag("Paddle"))
    {
      kickSound.Play();
      float horizontalInput = Input.GetAxis("Horizontal");
      rigidbody.AddForce(Vector3.right * paddleEffectAmmount * horizontalInput);
    }
  }

  public void Shoot()
  {
    rigidbody.AddForce(Input.GetAxis("Horizontal") * 4, 4, 0, ForceMode.VelocityChange);
  }
}
