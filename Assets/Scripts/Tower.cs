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

    // 범위 내의 가장 가까운 enemy를 찾음
    public Vector2 findTarget()
    {
        // 범위 안에 enemy가 없으면 default
        if (attackArea.enemyList.Count <= 0)
        {
            return default;
        }



        // return값으로 사용할 벡터
        Vector2 resultVec = Vector2.zero;
        float nearDistance = Mathf.Infinity;

        foreach(GameObject enemy in attackArea.enemyList)
        {
            // enemy Positoin - Tower Position  // 리스트에 있는 enemy와의 거리를 구함
            Vector2 enemyDir = enemy.transform.position - transform.position;
            float distance = enemyDir.sqrMagnitude; // 제곱근 계산을 피하기 위해 제곱된 거리 사용

            // 더 작은 값을 return 값으로
            if (distance < nearDistance)
            {
                nearDistance = distance;
                resultVec = enemyDir.normalized;
            }
        }

        // 찾은 결과 return
        return resultVec;
    }
}
