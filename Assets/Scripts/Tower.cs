using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int hp;

    public AttackArea attackArea;

    void Start()
    {

    }

    void Update()
    {

    }

    public void plusHP(int plusValue)
    {
        hp += plusValue;
    }

    public void minusHP(int minusValue)
    {
        hp -= minusValue;
    }

    // ���� ���� ���� ����� enemy�� ã��
    public Vector2 findTarget()
    {
        // ���� �ȿ� enemy�� ������ default
        if (attackArea.enemyList.Count <= 0)
        {
            return default;
        }



        // return������ ����� ����
        Vector2 resultVec = Vector2.zero;
        float nearDistance = Mathf.Infinity;

        foreach(GameObject enemy in attackArea.enemyList)
        {
            // enemy Positoin - Tower Position  // ����Ʈ�� �ִ� enemy���� �Ÿ��� ����
            Vector2 enemyDir = enemy.transform.position - transform.position;
            float distance = enemyDir.sqrMagnitude; // ������ ����� ���ϱ� ���� ������ �Ÿ� ���

            // �� ���� ���� return ������
            if (distance < nearDistance)
            {
                nearDistance = distance;
                resultVec = enemyDir.normalized;
            }
        }

        // ã�� ��� return
        return resultVec;
    }
}
