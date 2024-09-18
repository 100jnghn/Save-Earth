using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // �浹 ������ ���� Enemy�� List�� ����
    public List<GameObject> enemyList;

    void Start()
    {
        enemyList = new List<GameObject>();    
    }

    void Update()
    {
        
    }

    // Attack Area�� enemy�� ������ List�� �߰�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    // Enemy�� ���� �� List���� enemy ����
    public void removeEnemy(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }
}
