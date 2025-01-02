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
        // GameManager ��������
        gameManager = FindObjectOfType<GameManager>();

        // Ÿ���� origin position�� �˱� ���� ������
        tower = FindObjectOfType<Tower>();

        // �ı��� �� enemyList���� �ڽ��� �����ϱ� ���� ������
        attackArea = FindObjectOfType<AttackArea>();

        // ������ �� ���� ��ȭ�� ���� ������
        sprite = FindObjectOfType<SpriteRenderer>();

        // Collider ������
        enemyCollider = FindObjectOfType<CircleCollider2D>();
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
        level = gameManager.level;

        setHP(level / 3 * 1.2f + 1.7f);
        setMoveSpeed(level / 3 * 0.7f + 0.8f);
        setDamage(1);
        setValue(level / 5 + 1);
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

        // ����ϴ� ���
        if (hp <= 0)
        {
            gameManager.addScore(1);    // ���� ����
            dieSound.Play();            // ���� ���

            // �̵� ���߱�
            dir = Vector2.zero;

            // ���� ����Ʈ ���
            GameObject explosion = Instantiate(destoyParticle);
            explosion.transform.SetParent(gameObject.transform, false);

            // ����Ʈ���� ����
            attackArea.removeEnemy(gameObject);

            sprite.enabled = false;
            enemyCollider.enabled = false;
            Destroy(gameObject, dieSound.clip.length);
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

            // Enemy ü�� ����
            minusHP(1);

            // ���� ��ȭ
            sprite.color = Color.black;
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
            

            // GameManager�� player �� �߰�
            gameManager.money += value;

            // money UI ���� �Լ� ȣ��
            gameManager.callUIManagerChangeTextMoney();
        }
    }
}
