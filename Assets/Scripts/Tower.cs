using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("----- Stats -----")]
    [SerializeField] float maxHP;   // �ִ� ü��
    [SerializeField] float hp;      // ���� ü��
    [SerializeField] float attackTime;  float currentTime;  // ���� �ӵ�
    [SerializeField] float criticalChance;

    public float criticalDamage;    // bullet���� �����Ͽ� ġ��Ÿ dmg ���
    public float bulletSpeed;       // bullet���� �����Ͽ� �ӵ� ���
    public float bulletPower;       // bullet���� �����Ͽ� �Ϲ� dmg ���

    int enemyCount;
    bool isAttackReady = true;

    [Header(" ----- Components -----")]
    public GameObject bullet;
    public GameObject originPos;
    public AttackArea attackArea;

    void Start()
    {
        init();
    }

    void init()
    {
        maxHP = hp = 20;        // ü�� �ʱⰪ ����
        attackTime = 3f;        // ���� �ӵ� �ʱⰪ ����
        bulletSpeed = 1f;       // ����ü �ӵ� �ʱⰪ ����
        bulletPower = 1f;       // ���ݷ� �ʱⰪ ����
        criticalChance = 0f;    // ġ��Ÿ Ȯ�� �ʱⰪ ����
        criticalDamage = 0.1f;  // ġ��Ÿ ������ �ʱⰪ ����
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

    // ġ��Ÿ�� ����� bulletPower ���
    public float calcualteDamage()
    {
        float damage = bulletPower;

        // ġ��Ÿ Ȯ���� 0�̸� �׳� bulletPower return
        if (criticalChance <= 0f)
        {
            return damage;
        }

        // ���� ���� �̿��� ġ��Ÿ ���� ���� �Ǵ�
        bool isCritical = Random.Range(0f, 100f) <= criticalChance;

        // ġ��Ÿ�� ��� ������ ����
        if (isCritical)
        {
            damage += bulletPower * criticalDamage;
        }

        return damage;
    }

    private void doFire()
    {
        Instantiate(bullet, originPos.transform.position, originPos.transform.rotation);

        currentTime = 0;
        isAttackReady = false;
    }

    // Attack Area�� enemyList�� ũ�⸦ ��ȯ
    private int getEnemyListCount()
    {
        return attackArea.enemyList.Count;
    }

    public void plusHP(float plusValue)
    {
        hp += plusValue;
    }

    public void minusHP(float minusValue)
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
