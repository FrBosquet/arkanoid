using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  public new GameObject light;
  public bool flying = true;
  public float paddleEffectAmmount;
  public float wallRepulsion = 10.0f;
  public float slowRepulsion = 10.0f;

  private float minimumSpeed = 2;
  private new Rigidbody rigidbody;
  private AudioSource kickSound;
  private Vector3 lastVelocity;
  private string lastCollided;

  private float SPREAD = 0.2f;

  private void Awake()
  {
    Ball[] balls = FindObjectsOfType<Ball>();
    if (balls.Length > 2)
    {
      light.gameObject.SetActive(false);
    }
  }

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    kickSound = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Limit"))
    {
      GameObject.Find("GameManager").GetComponent<GameManager>().LoseBall();
      Destroy(gameObject);
    }
    else if (other.gameObject.CompareTag("Brick"))
    {
      Brick brick = other.gameObject.GetComponent<Brick>();
      brick.Hit();
    }
  }

  private void FixedUpdate()
  {
    if (rigidbody.velocity.magnitude < minimumSpeed && flying)
    {
      rigidbody.AddForce(rigidbody.velocity.normalized * slowRepulsion);
    }

    if (transform.position.x > 2.8)
    {
      rigidbody.AddForce(Vector3.left * wallRepulsion);
    }

    if (transform.position.x < -2.8)
    {
      rigidbody.AddForce(Vector3.right * wallRepulsion);
    }

    if (transform.position.y > 5)
    {
      rigidbody.AddForce(Vector3.down * wallRepulsion);
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    lastCollided = other.gameObject.tag;
    lastVelocity = rigidbody.velocity;

    if (other.gameObject.CompareTag("Brick"))
    {
      Brick brick = other.gameObject.GetComponent<Brick>();
      brick.Hit();
    }
    else if (other.gameObject.CompareTag("Paddle"))
    {
      kickSound.Play();
      float horizontalInput = Input.GetAxis("Horizontal");
      rigidbody.AddForce(Vector3.right * paddleEffectAmmount * horizontalInput);
    }
    else
    {
      kickSound.Play();
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
