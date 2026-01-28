using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public int stageLevel = 1;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject dicePrefab;
    public Transform[] spawnPoints;

    private float timer = 0f;
    private bool bossSpawned = false;

    void Update()
    {
        if (timer < 30f)
        {
            timer += Time.deltaTime;
            // 예: 3초마다 잡몹 생성
            if (Mathf.FloorToInt(timer) % 3 == 0 && !IsInvoking("SpawnEnemy")) 
                Invoke("SpawnEnemy", 0.4f);
        }
        else if (!bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnEnemy()
    {
        int idx = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[idx].position, Quaternion.identity);
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        Debug.Log("보스 등장!");
        Instantiate(bossPrefab, spawnPoints[0].position, Quaternion.identity);
    }

    // 보스가 죽었을 때 호출될 함수
    public void OnBossDeath(Vector3 deathPosition)
    {
        Debug.Log($"{stageLevel}개의 주사위 생성!");
        for (int i = 0; i < stageLevel * 3; i++)
        {
            Vector3 spawnPos = deathPosition + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f));
            Instantiate(dicePrefab, spawnPos, Quaternion.identity);
        }
    }
}