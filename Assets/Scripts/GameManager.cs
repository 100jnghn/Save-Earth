using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;

    public float enemySpawnTime;
    public float stageRemainTime;

    public bool isGaming;
    public bool onEnemySpawn;

    [Header ("Conponents")]
    public EnemySpawnArea[] spawnAreas;

    void Start()
    {
        init();

        // enemy 생성
        InvokeRepeating("spawnEnemyInAreas", 1f, enemySpawnTime);
    }

    // 처음 시작할 때 초기화
    void init()
    {
        level = 1;
        enemySpawnTime = 5f;

        isGaming = true;
        onEnemySpawn = true;
    }

    // 랜덤으로 Spawn Areas에 있는 spawnEnemy() 호출
    void spawnEnemyInAreas()
    {
        // 게임종료 || enemy생성 상태 아님 || 스테이지 종료
        if (!isGaming || !onEnemySpawn || stageRemainTime <= 0f)
        {
            CancelInvoke("spawnEnemyInAreas");
        }

        int areaIndex = Random.Range(0, spawnAreas.Length);
        spawnAreas[areaIndex].spawnEnemy();

        /*if (isGaming && onEnemySpawn && stageRemainTime >= 0)
        {
            int areaIndex = Random.Range(0, spawnAreas.Length);
            spawnAreas[areaIndex].spawnEnemy();
        }*/
    }

    void Update()
    {
        
    }
}
