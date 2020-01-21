using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private new Rigidbody rigidbody;
  public float moveSpeed = 50;
  public GameObject paddle;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void Update()
  {
    float horizontalInput = Input.GetAxis("Horizontal");

    rigidbody.AddForce(moveSpeed * Vector3.right * Time.deltaTime * horizontalInput, ForceMode.Force);
  }

  private void LateUpdate()
  {
    paddle.transform.position = transform.position;
  }
}
