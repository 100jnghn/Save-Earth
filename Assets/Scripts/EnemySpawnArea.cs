using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public GameManager gameManager;

    [Header ("Point Infomation")]
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    [SerializeField] float minX;
    [SerializeField] float minY;

    [Header("Enemy Prefabs")]
    public GameObject[] enemies;

    public void spawnEnemy()
    {
        // ���� ���� ��ġ
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        // ������ ���� �������� enemy prefab ����
        int idx = 1;
        switch(gameManager.level)
        {
            case 0:
                idx = 1;
                break;

            case 1:
                idx = 1;
                break;

            case 2:
                idx = enemies.Length;
                break;

            case 3:
                idx = enemies.Length;
                break;

            default:
                idx = 1;

                break;

        }

        int enemyIndex = Random.Range(0, idx);

        // ���� ��ġ vector �����
        Vector2 spawnPoint = new Vector2(randX, randY) + new Vector2(transform.position.x, transform.position.y);

        // Debug.Log(gameObject.name + " " + spawnPoint);

        // enemy ����
        Instantiate(enemies[enemyIndex], spawnPoint, Quaternion.identity);
    }
}
