using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Tower tower;
    Vector2 dir;

    [SerializeField]
    float bulletSpeed;

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();    
    }

    void Start()
    {
        dir = tower.findTarget();
        bulletSpeed = tower.bulletSpeed;
    }

    void Update()
    {
        transform.Translate(dir * bulletSpeed * Time.deltaTime);
    }
}
