using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header ("----- Stats -----")]
    [SerializeField] int hp;
    [SerializeField] float attackTime;  float currentTime;
    public float bulletSpeed;

    int enemyCount;
    bool isAttackReady = true;

    [Header(" ----- Components -----")]
    public GameObject bullet;
    public GameObject originPos;
    public AttackArea attackArea;

    void Start()
    {

    }

    void Update()
    {
        // 타워가 포격 가능한 상태인지 체크
        checkAttackTimer();

        // 공격 가능한 상태일 때 enemyList에 Enemy가 있으면 doFire();
        if (isAttackReady && getEnemyListCount() > 0)
        {
            doFire();
        }
    }

    private void checkAttackTimer()
    {
        if (attackTime <= currentTime)
        {
            isAttackReady = true;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void doFire()
    {
        Instantiate(bullet, originPos.transform.position, originPos.transform.rotation);

        isAttackReady = false;
    }

    // Attack Area의 enemyList의 크기를 반환
    private int getEnemyListCount()
    {
        return attackArea.enemyList.Count;
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
        // return값으로 사용할 벡터
        Vector2 resultVec = Vector2.zero;
        float nearDistance = Mathf.Infinity;

        foreach(GameObject enemy in attackArea.enemyList)
        {
            // enemy Positoin - Tower Position  // 리스트에 있는 enemy와의 거리를 구함
            Vector2 enemyDir = enemy.transform.position - originPos.transform.position;
            float distance = enemyDir.sqrMagnitude; // 제곱근 계산을 피하기 위해 제곱된 거리 사용

            // 더 작은 값을 return 값으로
            if (distance < nearDistance)
            {
                nearDistance = distance;
                resultVec = enemyDir;
                resultVec.Normalize();
            }
        }

        // 찾은 결과 return
        return resultVec;
    }
}
