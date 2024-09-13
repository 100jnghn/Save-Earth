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

        // enemy ����
        InvokeRepeating("spawnEnemyInAreas", 1f, enemySpawnTime);
    }

    // ó�� ������ �� �ʱ�ȭ
    void init()
    {
        level = 1;
        enemySpawnTime = 5f;

        isGaming = true;
        onEnemySpawn = true;
    }

    // �������� Spawn Areas�� �ִ� spawnEnemy() ȣ��
    void spawnEnemyInAreas()
    {
        // �������� || enemy���� ���� �ƴ� || �������� ����
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
