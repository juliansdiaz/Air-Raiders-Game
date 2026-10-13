using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO[] waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(0), currentWave.GetStartingWaypoint().position, Quaternion.identity);
                    yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
                }
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        while (isLooping);
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
