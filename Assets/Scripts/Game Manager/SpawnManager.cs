using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Transform enemyPrefabs;
    public Transform spawnPoint;
    public Wave[] waves;

    public GameStatus gameStatus;

    public Text waveCountText;
    public Text countDownText;
    public float timeBetweenWaves = 8f;

    private float countDown = 3f;
    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            this.enabled = false;
            gameStatus.WinLevel();
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        countDownText.text = "Start In: " + string.Format("{0:0}", countDown);
        
        if (waveIndex > 0)
        {
            waveCountText.text = "Wave " + (waveIndex + 1);
        } else
        {
            waveCountText.text = "Wave " + 1;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        waveIndex++;
    }
    
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
