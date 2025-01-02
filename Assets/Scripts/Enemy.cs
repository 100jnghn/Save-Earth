using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AttackArea attackArea;
    Tower tower;
    GameManager gameManager;
    SpriteRenderer sprite;
    CircleCollider2D enemyCollider;

    public AudioSource dieSound;
    public GameObject destoyParticle;

    [SerializeField] int level;
    [SerializeField] float hp;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    [SerializeField] int value;

    Vector2 dir;

    void Awake()
    {
        // GameManager 가져오기
        gameManager = FindObjectOfType<GameManager>();

        // 타워의 origin position을 알기 위해 가져옴
        tower = FindObjectOfType<Tower>();

        // 파괴될 때 enemyList에서 자신을 제거하기 위해 가져옴
        attackArea = FindObjectOfType<AttackArea>();

        // 공격한 후 색상 변화를 위해 가져옴
        sprite = FindObjectOfType<SpriteRenderer>();

        // Collider 가져옴
        enemyCollider = FindObjectOfType<CircleCollider2D>();
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
        // GameManager 값에서 가져온 wave의 level 값
        level = gameManager.level;

        setHP(level / 3 * 1.2f + 1.7f);
        setMoveSpeed(level / 3 * 0.7f + 0.8f);
        setDamage(1);
        setValue(level / 5 + 1);
    }

    // Enemy 이동 방향 결정 (Tower가 있는 곳)
    void setDirection()
    {
        dir = tower.originPos.transform.position - gameObject.transform.position;
        dir.Normalize();

        // Debug.Log("Direction Vector: " + dir);
    }

    // HP 값 설정
    void setHP(float hpValue)
    {
        hp = hpValue;
    }

    // moveSpeed 값 설정
    void setMoveSpeed(float speedValue)
    {
        moveSpeed = speedValue;
    }

    // Damage 값 설정
    void setDamage(float damageValue)
    {
        damage = damageValue;
    }    
    // --------------------------------- //

    // Enemy value 값 설정 // 사망시 value를 player의 money에 더해줌
    void setValue(int v)
    {
        value = v;
    }

    // Tower를 향해 이동
    void moveToTower()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.Self);
    }

    // 데미지를 입음
    void minusHP(float v)
    {
        hp -= v;

        // 사망하는 경우
        if (hp <= 0)
        {
            gameManager.addScore(1);    // 점수 증가
            dieSound.Play();            // 사운드 재생

            // 이동 멈추기
            dir = Vector2.zero;

            // 폭발 이펙트 재생
            GameObject explosion = Instantiate(destoyParticle);
            explosion.transform.SetParent(gameObject.transform, false);

            // 리스트에서 제거
            attackArea.removeEnemy(gameObject);

            sprite.enabled = false;
            enemyCollider.enabled = false;
            Destroy(gameObject, dieSound.clip.length);
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

            // Enemy 체력 감소
            minusHP(1);

            // 색상 변화
            sprite.color = Color.black;
        }
    }

    // Trigger 검사
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 - Bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            minusHP(tower.calculateDamage());
        }
    }

    private void OnDestroy()
    {
        // AttackArea의 List에서 자신을 제거
        if (attackArea != null)
        {
            

            // GameManager의 player 돈 추가
            gameManager.money += value;

            // money UI 변경 함수 호출
            gameManager.callUIManagerChangeTextMoney();
        }
    }
}
