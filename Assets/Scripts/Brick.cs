using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
  public int startingLife;
  [SerializeField] private int life = 1;
  public GameObject particlePrefab;
  public Material[] materials;
  private MeshRenderer meshRenderer;
  private AudioSource audioSource;

  private void Awake()
  {
    meshRenderer = gameObject.GetComponent<MeshRenderer>();
    audioSource = gameObject.GetComponent<AudioSource>();
  }

  private void Start()
  {
    life = startingLife;
    UpdateMaterial();
  }

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
    else
    {
      audioSource.Play();
      UpdateMaterial();
    }
  }

  private void UpdateMaterial()
  {
    meshRenderer.material = materials[life - 1];
  }
}
