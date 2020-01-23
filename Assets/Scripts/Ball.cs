using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  public float paddleEffectAmmount;
  private new Rigidbody rigidbody;
  private AudioSource kickSound;
  public bool flying = true;
  private float SPREAD = 0.2f;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    kickSound = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
    GameObject.Find("GameManager").GetComponent<GameManager>().LoseBall();
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
    flying = true;
    rigidbody.AddForce(Input.GetAxis("Horizontal") * 4, 4, 0, ForceMode.VelocityChange);
  }

  public void Split()
  {
    if (!flying) return;

    Vector3 velocity = rigidbody.velocity;
    Vector3 perpendicular = Vector3.Cross(Vector3.forward, velocity);

    GameObject copy1 = Instantiate(gameObject, transform.position + perpendicular * Time.deltaTime, transform.rotation);
    GameObject copy2 = Instantiate(gameObject, transform.position - perpendicular * Time.deltaTime, transform.rotation);

    copy1.GetComponent<Rigidbody>().velocity = Vector3.LerpUnclamped(velocity, perpendicular, SPREAD);
    copy2.GetComponent<Rigidbody>().velocity = Vector3.LerpUnclamped(velocity, perpendicular * -1, SPREAD); ;

    Destroy(gameObject);

  }
}
