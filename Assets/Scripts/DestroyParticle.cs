using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
  private new ParticleSystem particleSystem;
  public float lifetime;

  private void Awake()
  {
    particleSystem = gameObject.GetComponent<ParticleSystem>();
    lifetime = (particleSystem.main.startLifetimeMultiplier);
  }

  private void Start()
  {
    StartCoroutine(DestroyAfterLifetime());
  }

  IEnumerator DestroyAfterLifetime()
  {
    yield return new WaitForSeconds(lifetime);
    Destroy(gameObject);
  }
}
