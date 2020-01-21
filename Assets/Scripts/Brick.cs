using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
  [SerializeField] private int life = 1;
  public GameObject particlePrefab;

  public void Hit()
  {
    life--;

    if (life == 0)
    {
      GameObject particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
      ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();

      particleSystem.Play();

      Destroy(gameObject);
    }
  }
}
