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
        // GameManager 가져오기

        attackArea = FindObjectOfType<AttackArea>();
    }

    void Start()
    {
        setDirection();     // 이동 방향 
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

    // Enemy 이동 방향 결정 (Tower가 있는 곳)
    void setDirection()
    {
        Transform tower = GameObject.Find("Tower").transform;
        dir = tower.position - transform.position;
        dir.Normalize();
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
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    // Collision 검사
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            // 이동하지 않도록 설정
            moveSpeed = 0;
            dir = Vector2.zero;

            collision.gameObject.GetComponent<Tower>().minusHP(damage);
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
