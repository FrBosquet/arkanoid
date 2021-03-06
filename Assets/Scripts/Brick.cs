﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
  public int startingLife;
  [SerializeField] private int life = 1;
  public GameManager gameManager;
  public GameObject particlePrefab;
  public GameObject PowerUp;
  public Material[] materials;
  private MeshRenderer meshRenderer;
  private AudioSource audioSource;
  public bool phantom = false;

  private void Awake()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    meshRenderer = gameObject.GetComponent<MeshRenderer>();
    audioSource = gameObject.GetComponent<AudioSource>();
  }

  private void Start()
  {
    life = startingLife;
    UpdateMaterial();

    if (phantom)
    {
      Collider collider = gameObject.GetComponent<Collider>();
      collider.isTrigger = true;

      Color color = meshRenderer.material.color;
      meshRenderer.material.color = new Color(color.r, color.g, color.b, 0f);
    }
  }

  public void Hit()
  {
    life--;

    if (life == 0)
    {
      Vector3 position = transform.position;

      GameObject particles = Instantiate(particlePrefab, position, Quaternion.identity);
      ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();

      if (PowerUp != null)
      {
        Instantiate(PowerUp, position, Quaternion.identity);
      }

      gameManager.DestroyBrick();
      gameManager.AddPoints(100);
      Destroy(gameObject);
    }
    else
    {
      gameManager.AddPoints(50);
      audioSource.Play();
      UpdateMaterial();
    }
  }

  private void UpdateMaterial()
  {
    meshRenderer.material = materials[life - 1];
  }
}
