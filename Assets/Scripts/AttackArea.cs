using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // 충돌 범위에 들어온 Enemy를 List에 저장
    public List<GameObject> enemyList;

    void Start()
    {
        enemyList = new List<GameObject>();    
    }

    void Update()
    {
        
    }

    // Attack Area에 enemy가 들어오면 List에 추가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    // Enemy가 죽을 때 List에서 enemy 제거
    public void removeEnemy(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }
}
