using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List <WaveConfig> waveConfigs;
    [SerializeField] bool looping = false ; 
    int startingWaves = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }
    private IEnumerator SpawnAllWaves()
    {
        for (int j=0; j< waveConfigs.Count; j++)
        {
            var currentWave = waveConfigs[j];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        } 
    }
    private IEnumerator SpawnAllEnemiesInWave (WaveConfig waveConfig )
    {
        for (int i = 0; i<= waveConfig.GetNumbersOfEnemies() - 1; i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnTime());
        }
    }
}
