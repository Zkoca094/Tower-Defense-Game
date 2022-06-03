using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Wave
{

    public GameObject enemy;
    public int count;
    public float rate;

}
public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveIndex = 0;
    public Text timerText;
    public GameManager manager;
    public int GetWaveIndex()
    {
        return waves.Length;
    }
    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            manager = GetComponent<GameManager>();
            manager.WinLevel();
            this.enabled = false;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        timerText.text = string.Format("{0:00:00}", countDown);
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
