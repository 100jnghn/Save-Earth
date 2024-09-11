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
        // Ÿ���� ���� ������ �������� üũ
        checkAttackTimer();

        // ���� ������ ������ �� enemyList�� Enemy�� ������ doFire();
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

    // Attack Area�� enemyList�� ũ�⸦ ��ȯ
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

    // ���� ���� ���� ����� enemy�� ã��
    public Vector2 findTarget()
    {
        // return������ ����� ����
        Vector2 resultVec = Vector2.zero;
        float nearDistance = Mathf.Infinity;

        foreach(GameObject enemy in attackArea.enemyList)
        {
            // enemy Positoin - Tower Position  // ����Ʈ�� �ִ� enemy���� �Ÿ��� ����
            Vector2 enemyDir = enemy.transform.position - originPos.transform.position;
            float distance = enemyDir.sqrMagnitude; // ������ ����� ���ϱ� ���� ������ �Ÿ� ���

            // �� ���� ���� return ������
            if (distance < nearDistance)
            {
                nearDistance = distance;
                resultVec = enemyDir;
                resultVec.Normalize();
            }
        }

        // ã�� ��� return
        return resultVec;
    }
}
