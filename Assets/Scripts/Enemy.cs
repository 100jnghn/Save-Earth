using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AttackArea attackArea;
    Tower tower;

    [SerializeField] int level;
    [SerializeField] float hp;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;

    Vector2 dir;

    void Awake()
    {
        // GameManager 가져오기

        // 타워의 origin position을 알기 위해 가져옴
        tower = FindAnyObjectByType<Tower>();

        // 파괴될 때 enemyList에서 자신을 제거하기 위해 가져옴
        attackArea = FindObjectOfType<AttackArea>();

            
    }

    void Start()
    {
        // 이동 방향 
        setDirection();

        init();             // 초기값 설정
    }

    void Update()
    {
        moveToTower();
    }



    // ---------- 초기값 설정 ---------- //
    void init()
    {
        level = 1;  // GameManager 값에서 가져온 wave 값

        switch(level)
        {
            case 0:
                setHP(1);
                setMoveSpeed(1);
                setDamage(1);
                break;
            
            case 1:
                setHP(1);
                setMoveSpeed(1);
                setDamage(1);
                break;

            case 2:
                setHP(1);
                setMoveSpeed(1);
                setDamage(1);
                break;

            case 3:
                setHP(1);
                setMoveSpeed(1);
                setDamage(1);
                break;

            default:
                break;
        }
    }

    // Enemy 이동 방향 결정 (Tower가 있는 곳)
    void setDirection()
    {
        dir = tower.originPos.transform.position - gameObject.transform.position;
        dir.Normalize();
        Debug.Log("Direction Vector: " + dir);
    }

    // HP 값 설정
    void setHP(int hpValue)
    {
        hp = hpValue;
    }

    // moveSpeed 값 설정
    void setMoveSpeed(int speedValue)
    {
        moveSpeed = speedValue;
    }

    // Damage 값 설정
    void setDamage(int damageValue)
    {
        damage = damageValue;
    }    
    // --------------------------------- //



    // Tower를 향해 이동
    void moveToTower()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.Self);
    }

    // 데미지를 입음
    void minusHP(float v)
    {
        hp -= v;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Collision 검사
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 - Tower
        if (collision.gameObject.CompareTag("Tower"))
        {
            // 이동하지 않도록 설정
            moveSpeed = 0;
            dir = Vector2.zero;

            collision.gameObject.GetComponent<Tower>().minusHP(damage);
        }
    }

    // Trigger 검사
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 - Bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            float bulletPower = collision.GetComponent<Bullet>().getBulletPower();
            Destroy(collision.gameObject);

            minusHP(bulletPower);
        }
    }

    private void OnDestroy()
    {
        // AttackArea의 List에서 자신을 제거
        if (attackArea != null)
        {
            attackArea.removeEnemy(gameObject);
        }
    }
}
