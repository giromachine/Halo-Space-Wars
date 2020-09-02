using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;

    Vector3 BansheeView = new Vector3(0, 0, 180f);

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(AllWaves());
    }
    private IEnumerator AllWaves()
    {
        for (int index = startingWave; index < waveConfigs.Count; index++)
        {
            var currentWave = waveConfigs[index];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int count = 0; count < waveConfig.GetNumberOfEnemies(); count++) 
        { 
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.Euler(BansheeView));
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}

