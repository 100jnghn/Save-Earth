using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;

    [Header ("----- Player -----")]
    public int money;

    [Header ("----- Enemy -----")]
    public float enemySpawnTime;
    public float stageRemainTime;

    [Header ("----- Boolean -----")]
    public bool isGaming;
    public bool onEnemySpawn;

    [Header ("----- Conponents -----")]
    public EnemySpawnArea[] spawnAreas;
    public UIManager uiManager;


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
        enemySpawnTime = 4.5f;
        stageRemainTime = 20f;

        isGaming = true;
        onEnemySpawn = true;

        uiManager.changeStage(level);
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

    void levelUp()
    {
        level += 1;

        float timeDecreaseValue;
        if (level <=3)
        {
            timeDecreaseValue = 0.3f;
        }
        else if (level <= 7)
        {
            timeDecreaseValue = 0.2f;
        }
        else
        {
            timeDecreaseValue = 0.1f;
        }


        if (enemySpawnTime > 0.5f)
        {
            enemySpawnTime -= timeDecreaseValue;
        }

        uiManager.changeStage(level);
    }

    // UIManager�� changeTextMoney()�� ȣ��
    // enemy�� UIManager�� �ƴ� GameManager�� ���� ���� �����ϱ� ������ ������ �Լ� ����
    public void callUIManagerChangeTextMoney()
    {
        uiManager.changeTextMoney();
    }

    void Update()
    {
        // �������� ���� �ð� ����
        decreaseRemainTime();
    }

    void decreaseRemainTime()
    {
        uiManager.showRemainTime(stageRemainTime);

        if (stageRemainTime >= 0f)
        {
            stageRemainTime -= Time.deltaTime;
        }
        else
        {
            levelUp();

            // ���� �������� �ð� ����
            stageRemainTime = level * 5 + 10;
        }
        
    } 
}
