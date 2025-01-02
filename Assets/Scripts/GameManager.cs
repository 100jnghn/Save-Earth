using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;

    int highScore;
    int myScore;
    
    float timeDecreaseValue = 0.2f;

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
        InvokeRepeating("spawnEnemyInAreas", 0f, enemySpawnTime);
    }

    // ó�� ������ �� �ʱ�ȭ
    void init()
    {
        // Ÿ�ӽ����� 1
        Time.timeScale = 1;

        // �ְ� ���� �ҷ�����
        highScore = PlayerPrefs.GetInt("HighScore");
        myScore = 0;

        // UI ���� ȣ��
        uiManager.gameStart();

        level = 1;
        enemySpawnTime = 4.0f;
        stageRemainTime = 20f;

        isGaming = true;
        onEnemySpawn = true;

        uiManager.changeStage(level);
    }

    // ���� ���� ����
    public void gameEnd()
    {
        // �ְ� ��� ���� �˻�
        if (myScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", myScore);
        }

        // ���� ���� UI 
        uiManager.gameEnd(highScore, myScore);

        // Ÿ�ӽ����� 0
        Time.timeScale = 0;
    }

    // �� ���� ���� (Enemy óġ ��)
    public void addScore(int value)
    {
        myScore += value;

        uiManager.changeTextMyScore(myScore);
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

        timeDecreaseValue += 0.1f;

        if (enemySpawnTime - timeDecreaseValue > 0.02f)
        {
            enemySpawnTime -= timeDecreaseValue;
        }

        if (enemySpawnTime < 0.75f)
        {
            enemySpawnTime -= 0.1f;
        }

        if (enemySpawnTime <= 0.05f)
        {
            enemySpawnTime = 0.05f;
        }

        Debug.Log("Enemy Spawn Time : " + enemySpawnTime);

        // ���ο� Enemy Spawn InvokeRepeating ����
        CancelInvoke("spawnEnemyInAreas");
        InvokeRepeating("spawnEnemyInAreas", 0.5f, enemySpawnTime);

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
