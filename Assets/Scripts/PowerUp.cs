using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  protected GameManager gameManager;
  public GameObject particleEffect;
  public float fallSpeed;
  public float rotateSpeed;

  private void Awake()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  void Update()
  {
    transform.position += Vector3.down * Time.deltaTime * fallSpeed;
    transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
  }

  private void OnTriggerEnter(Collider other)
  {
    Destroy(gameObject);
  }

  private void OnCollisionEnter(Collision other)
  {
    Debug.Log("Get powerup");
    GameObject particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
