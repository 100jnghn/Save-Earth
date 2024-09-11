using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AttackArea attackArea;

    [SerializeField] int level;
    [SerializeField] int hp;
    [SerializeField] int moveSpeed;
    [SerializeField] int damage;

    Vector2 dir;

    void Awake()
    {
        // GameManager ��������

        attackArea = FindObjectOfType<AttackArea>();
    }

    void Start()
    {
        setDirection();     // �̵� ���� 
        init();             // �ʱⰪ ����
    }

    void Update()
    {
        moveToTower();
    }



    // ---------- �ʱⰪ ���� ---------- //

    void init()
    {
        level = 1;  // GameManager ������ ������ wave ��

        switch(level)
        {
            case 0:
                setHP(10);
                setMoveSpeed(1);
                setDamage(1);
                break;
            
            case 1:
                setHP(10);
                setMoveSpeed(1);
                setDamage(1);
                break;

            case 2:
                setHP(10);
                setMoveSpeed(1);
                setDamage(1);
                break;

            case 3:
                setHP(10);
                setMoveSpeed(1);
                setDamage(1);
                break;

            default:
                break;
        }
    }

    // Enemy �̵� ���� ���� (Tower�� �ִ� ��)
    void setDirection()
    {
        Transform tower = GameObject.Find("Tower").transform;
        dir = tower.position - transform.position;
        dir.Normalize();
    }

    // HP �� ����
    void setHP(int hpValue)
    {
        hp = hpValue;
    }

    // moveSpeed �� ����
    void setMoveSpeed(int speedValue)
    {
        moveSpeed = speedValue;
    }

    // Damage �� ����
    void setDamage(int damageValue)
    {
        damage = damageValue;
    }    
    // --------------------------------- //



    // Tower�� ���� �̵�
    void moveToTower()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    // Collision �˻�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            // �̵����� �ʵ��� ����
            moveSpeed = 0;
            dir = Vector2.zero;

            collision.gameObject.GetComponent<Tower>().minusHP(damage);
        }
    }

    private void OnDestroy()
    {
        // AttackArea�� List���� �ڽ��� ����
        if (attackArea != null)
        {
            attackArea.removeEnemy(gameObject);
        }
    }
}
