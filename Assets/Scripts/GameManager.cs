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
        stageRemainTime = 30f;

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

    void levelChange()
    {

    }

    // UIManager�� changeTextMoney()�� ȣ��
    // enemy�� UIManager�� �ƴ� GameManager�� ���� ���� �����ϱ� ������ ������ �Լ� ����
    public void callUIManagerChangeTextMoney()
    {
        uiManager.changeTextMoney();
    }

    void Update()
    {
        
    }
}
