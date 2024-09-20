using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Tower tower;
    Vector2 dir;

    [SerializeField] float bulletSpeed;

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();    
    }

    void Start()
    {
        dir = tower.findTarget();               // bullet이 날아갈 방향
        bulletSpeed = tower.bulletSpeed;        // bullet이 날아갈 속도
    }

    void Update()
    {
        transform.Translate(dir * bulletSpeed * Time.deltaTime);
    }
}
