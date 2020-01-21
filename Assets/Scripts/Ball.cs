﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  private new Rigidbody rigidbody;
  private AudioSource kickSound;

  void Start()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
    rigidbody.AddForce(4, 4, 0, ForceMode.VelocityChange);
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


  }
}
