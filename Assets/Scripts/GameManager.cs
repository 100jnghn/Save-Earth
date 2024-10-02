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

        // enemy 생성
        InvokeRepeating("spawnEnemyInAreas", 1f, enemySpawnTime);
    }

    // 처음 시작할 때 초기화
    void init()
    {
        level = 1;
        enemySpawnTime = 4.5f;
        stageRemainTime = 20f;

        isGaming = true;
        onEnemySpawn = true;

        uiManager.changeStage(level);
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

    // UIManager의 changeTextMoney()를 호출
    // enemy가 UIManager가 아닌 GameManager를 통해 값에 접근하기 때문에 별도로 함수 제작
    public void callUIManagerChangeTextMoney()
    {
        uiManager.changeTextMoney();
    }

    void Update()
    {
        // 스테이지 남은 시간 감소
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

            // 다음 스테이지 시간 설정
            stageRemainTime = level * 5 + 10;
        }
        
    } 
}
