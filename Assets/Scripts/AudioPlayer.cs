using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;
    [SerializeField] AudioClip bulletImpactClip;
    [SerializeField][Range(0f, 1f)] float bulletImpactVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    [Header("Spawn")]
    [SerializeField] AudioClip spawnClip;
    [SerializeField][Range(0f, 1f)] float spawnVolume = 1f;

    [Header("Blob")]
    [SerializeField] AudioClip blobAttackClip;
    [SerializeField][Range(0f, 1f)] float blobAttackVolume = 1f;
    [SerializeField] AudioClip blobDieClip;
    [SerializeField][Range(0f, 1f)] float blobDieVolume = 1f;

    [Header("Skra")]
    [SerializeField] AudioClip skraClip;
    [SerializeField][Range(0f, 1f)] float skraVolume = 1f;
    [SerializeField] AudioClip skraDieClip;
    [SerializeField][Range(0f, 1f)] float skraDieVolume = 1f;

    // static AudioPlayer instance;
    /*   void Awake()
      {
          ManageSingleton();
      }

      void ManageSingleton()
      {
          if (instance != null)
          {
              //gameObject.SetActive(false);
              Destroy(gameObject);
          }
          else
          {
              instance = this;
              DontDestroyOnLoad(gameObject);
          }
      } */

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void BulletImpactClip()
    {
        PlayClip(bulletImpactClip, bulletImpactVolume);
    }

    public void DamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlaySpawnClip()
    {
        PlayClip(spawnClip, spawnVolume);
    }

    public void BlobAttackClip()
    {
        PlayClip(blobAttackClip, blobAttackVolume);
    }

    public void BlobDieClip()
    {
        PlayClip(blobDieClip, blobDieVolume);
    }

    public void SkraClip()
    {
        PlayClip(skraClip, skraVolume);
    }

    public void SkraDieClip()
    {
        PlayClip(skraDieClip, skraDieVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
