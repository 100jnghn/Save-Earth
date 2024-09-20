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
        dir = tower.findTarget();               // bullet�� ���ư� ����
        bulletSpeed = tower.bulletSpeed;        // bullet�� ���ư� �ӵ�
        bulletPower = tower.calcualteDamage();  // bullet�� ������ ���
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
