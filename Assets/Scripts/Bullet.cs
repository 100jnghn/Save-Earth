using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Tower tower;
    Vector2 dir;

    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletPower;

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();    
    }

    void Start()
    {
        dir = tower.findTarget();               // bullet이 날아갈 방향
        bulletSpeed = tower.bulletSpeed;        // bullet이 날아갈 속도
        bulletPower = tower.calcualteDamage();  // bullet의 데이지 계산
    }

    void Update()
    {
        transform.Translate(dir * bulletSpeed * Time.deltaTime);
    }

    public float getBulletPower()
    {
        return bulletPower;
    }
}
