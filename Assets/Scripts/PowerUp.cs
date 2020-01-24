using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  protected GameManager gameManager;
  protected Player player;
  public GameObject particleEffect;
  private float fallSpeed = 1;
  private float rotateSpeed = 120;

  private void Awake()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    player = GameObject.Find("Player").GetComponent<Player>();
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

  protected virtual void Effect()
  {
    return;
  }

  private void OnCollisionEnter(Collision other)
  {
    Effect();
    GameObject particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
