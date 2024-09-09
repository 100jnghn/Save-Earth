using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int hp;

    void Start()
    {

    }

    void Update()
    {

    }

    public void plusHP(int plusValue)
    {
        hp += plusValue;
    }

    public void minusHP(int minusValue)
    {
        hp -= minusValue;
    }
}
