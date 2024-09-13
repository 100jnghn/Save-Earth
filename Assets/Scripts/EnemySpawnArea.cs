using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    [Header ("Point Infomation")]
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    [SerializeField] float minX;
    [SerializeField] float minY;

    [Header("Enemy Prefabs")]
    public GameObject[] enemies;

    public void spawnEnemy()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        int enemyIndex = Random.Range(0, enemies.Length);

        Vector2 spawnPoint = new Vector2(randX, randY) + new Vector2(transform.position.x, transform.position.y);

        Debug.Log(gameObject.name + " " + spawnPoint);

        Instantiate(enemies[enemyIndex], spawnPoint, Quaternion.identity);
    }
}
