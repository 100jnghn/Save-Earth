using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AttackArea attackArea;
    Tower tower;
    GameManager gameManager;

    [SerializeField] int level;
    [SerializeField] float hp;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    [SerializeField] int value;

    Vector2 dir;

    void Awake()
    {
        // GameManager ��������
        gameManager = FindObjectOfType<GameManager>();

        // Ÿ���� origin position�� �˱� ���� ������
        tower = FindObjectOfType<Tower>();

        // �ı��� �� enemyList���� �ڽ��� �����ϱ� ���� ������
        attackArea = FindObjectOfType<AttackArea>();

            
    }

    void Start()
    {
        // �̵� ���� 
        setDirection();

        init();             // �ʱⰪ ����
    }

    void Update()
    {
        moveToTower();
    }



    // ---------- �ʱⰪ ���� ---------- //
    void init()
    {
        // GameManager ������ ������ wave�� level ��
        switch(gameManager.level)
        {
            case 0:
                setHP(1);
                setMoveSpeed(0.8f);
                setDamage(1);
                setValue(1);
                break;
            
            case 1:
                setHP(1);
                setMoveSpeed(0.8f);
                setDamage(1);
                setValue(1);
                break;

            case 2:
                setHP(1);
                setMoveSpeed(0.8f);
                setDamage(1);
                setValue(1);
                break;

            case 3:
                setHP(1);
                setMoveSpeed(0.8f);
                setDamage(1);
                setValue(1);
                break;

            default:
                break;
        }
    }

    // Enemy �̵� ���� ���� (Tower�� �ִ� ��)
    void setDirection()
    {
        dir = tower.originPos.transform.position - gameObject.transform.position;
        dir.Normalize();

        // Debug.Log("Direction Vector: " + dir);
    }

    // HP �� ����
    void setHP(float hpValue)
    {
        hp = hpValue;
    }

    // moveSpeed �� ����
    void setMoveSpeed(float speedValue)
    {
        moveSpeed = speedValue;
    }

    // Damage �� ����
    void setDamage(float damageValue)
    {
        damage = damageValue;
    }    
    // --------------------------------- //

    // Enemy value �� ���� // ����� value�� player�� money�� ������
    void setValue(int v)
    {
        value = v;
    }

    // Tower�� ���� �̵�
    void moveToTower()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.Self);
    }

    // �������� ����
    void minusHP(float v)
    {
        hp -= v;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Collision �˻�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 - Tower
        if (collision.gameObject.CompareTag("Tower"))
        {
            // �̵����� �ʵ��� ����
            moveSpeed = 0;
            dir = Vector2.zero;

            collision.gameObject.GetComponent<Tower>().minusHP(damage);
        }
    }

    // Trigger �˻�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 - Bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            minusHP(tower.calculateDamage());
        }
    }

    private void OnDestroy()
    {
        // AttackArea�� List���� �ڽ��� ����
        if (attackArea != null)
        {
            attackArea.removeEnemy(gameObject);

            // GameManager�� player �� �߰�
            gameManager.money += value;

            // money UI ���� �Լ� ȣ��
            gameManager.callUIManagerChangeTextMoney();
        }
    }
}
