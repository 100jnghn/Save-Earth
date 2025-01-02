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

        // enemy 생성
        InvokeRepeating("spawnEnemyInAreas", 0f, enemySpawnTime);
    }

    // 처음 시작할 때 초기화
    void init()
    {
        // 타임스케일 1
        Time.timeScale = 1;

        // 최고 점수 불러오기
        highScore = PlayerPrefs.GetInt("HighScore");
        myScore = 0;

        // UI 시작 호출
        uiManager.gameStart();

        level = 1;
        enemySpawnTime = 4.0f;
        stageRemainTime = 20f;

        isGaming = true;
        onEnemySpawn = true;

        uiManager.changeStage(level);
    }

    // 게임 종료 로직
    public void gameEnd()
    {
        // 최고 기록 갱신 검사
        if (myScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", myScore);
        }

        // 게임 종료 UI 
        uiManager.gameEnd(highScore, myScore);

        // 타임스케일 0
        Time.timeScale = 0;
    }

    // 내 점수 증가 (Enemy 처치 시)
    public void addScore(int value)
    {
        myScore += value;

        uiManager.changeTextMyScore(myScore);
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

        // 새로운 Enemy Spawn InvokeRepeating 설정
        CancelInvoke("spawnEnemyInAreas");
        InvokeRepeating("spawnEnemyInAreas", 0.5f, enemySpawnTime);

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
