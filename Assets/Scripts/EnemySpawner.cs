using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 3f;
    [SerializeField] float timeBetweenEnemies = 0.3f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping = true;

    AudioPlayer audioPlayer;
    [SerializeField] ParticleSystem spawnFX;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;

                for (int index = 0; index < currentWave.GetEnemyCount(); index++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(index), currentWave.GetStartingPoints(index).position, transform.rotation, transform);
                    Instantiate(spawnFX, currentWave.GetStartingPoints(index).position, transform.rotation);
                    audioPlayer.PlaySpawnClip();
                    spawnFX.Play();
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
                yield return new WaitUntil(() =>
                   {
                       return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
                   });

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
}
