using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("----- Associations -----")]
    public GameManager gameManager;
    public UIManager uiManager;

    [Header("----- Stats -----")]
    [SerializeField] float maxHP;   // �ִ� ü��
    [SerializeField] float hp;      // ���� ü��
    public float attackTime;  float currentTime;  // ���� �ӵ�
    public int criticalChance;      // ġ��Ÿ Ȯ��
    public float criticalDamage;    // ġ��Ÿ dmg ���
    public float bulletSpeed;       // bullet���� �����Ͽ� �ӵ� ���
    public float bulletPower;       // �Ϲ� dmg

    int enemyCount;
    bool isAttackReady = true;

    [Header(" ----- Components -----")]
    public GameObject bullet;
    public GameObject originPos;
    public AttackArea attackArea;

    [Header("----- Sounds ----- ")]
    public AudioSource attackSound;



    void Start()
    {
        init();
    }

    void init()
    {
        maxHP = hp = 20;        // ü�� �ʱⰪ ����
        attackTime = 3f;        // ���� �ӵ� �ʱⰪ ����
        bulletSpeed = 1f;       // ����ü �ӵ� �ʱⰪ ����
        bulletPower = 2f;       // ���ݷ� �ʱⰪ ����
        criticalChance = 0;     // ġ��Ÿ Ȯ�� �ʱⰪ ����
        criticalDamage = 1f;    // ġ��Ÿ ������ �ʱⰪ ����
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
    public float calculateDamage()
    {
        float damage = bulletPower;

        // ġ��Ÿ Ȯ���� 0�̸� �׳� bulletPower return
        if (criticalChance <= 0)
        {
            return damage;
        }

        // ���� ���� �̿��� ġ��Ÿ ���� ���� �Ǵ�
        bool isCritical = Random.Range(0, 100) < criticalChance;

        // ġ��Ÿ�� ��� ������ ����
        if (isCritical)
        {
            damage = bulletPower * criticalDamage;
        }

        return damage;
    }

    // ����
    private void doFire()
    {
        // Bulelt ����
        Instantiate(bullet, originPos.transform.position, originPos.transform.rotation);

        // ���� ���
        attackSound.Play();

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

        // HP Bar UI�� ����� ü�� �ݿ�
        uiManager.checkHP(maxHP, hp);

        if (hp <=0)
        {
            gameManager.gameEnd();
        }
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
