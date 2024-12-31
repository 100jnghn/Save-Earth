using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // 충돌 범위에 들어온 Enemy를 List에 저장
    public List<GameObject> enemyList;

    // Mesh Renderer
    SpriteRenderer mesh;

    // Collider
    CircleCollider2D circleCollider;


    void Start()
    {
        enemyList = new List<GameObject>(); // 공격 범위에 들어온 적들을 담아주는 Llist
        mesh = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    // Attack Area에 enemy가 들어오면 List에 추가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    // Enemy가 죽을 때 List에서 enemy 제거
    public void removeEnemy(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    // AttackArea의 크기 증가
    public void expandArea(float expandValue)
    {
        // 현재 scale 값을 가져옴
        Vector2 sizeVector = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        
        // 현재 scale 값에 증가값 v를 더해줌
        sizeVector.x += expandValue;
        sizeVector.y += expandValue;

        // Resize
        mesh.transform.localScale = sizeVector;
        circleCollider.transform.localScale = sizeVector;
    }
}
