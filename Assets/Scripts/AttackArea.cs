using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // �浹 ������ ���� Enemy�� List�� ����
    public List<GameObject> enemyList;

    // Mesh Renderer
    SpriteRenderer mesh;

    // Collider
    CircleCollider2D circleCollider;


    void Start()
    {
        enemyList = new List<GameObject>(); // ���� ������ ���� ������ ����ִ� Llist
        mesh = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    // Attack Area�� enemy�� ������ List�� �߰�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    // Enemy�� ���� �� List���� enemy ����
    public void removeEnemy(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    // AttackArea�� ũ�� ����
    public void expandArea(float expandValue)
    {
        // ���� scale ���� ������
        Vector2 sizeVector = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        
        // ���� scale ���� ������ v�� ������
        sizeVector.x += expandValue;
        sizeVector.y += expandValue;

        // Resize
        mesh.transform.localScale = sizeVector;
        circleCollider.transform.localScale = sizeVector;
    }
}
