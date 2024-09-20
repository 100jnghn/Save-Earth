using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header ("UI Components Player")]
    public Text textMoney;  // Text - 현재 money

    [Header("UI Components Panels")]
    public GameObject panelAttack;
    public GameObject panelHP;

    [Header("UI Components Tab Buttons")]
    public Button btnAttack;
    public Button btnHP;

    [Header("----- UI Components Attack -----")]
    public Button btnAttackPower;       // btn - 공격력 증가
    public Button btnAttackSpeed;       // btn - 공격 속도 증가
    public Button btnAttackRange;       // btn - 공격 범위 증가
    public Button btnBulletSpeed;       // btn - 투사체 속도 증가
    public Button btnCriticalChance;    // btn - 치명타 확률 증가
    public Button btnCriticalDamage;    // btn - 치명타 데미지 증가

    [Header("UI Components Health")]
    public Button btnHPHeal;
    public Button btnHPIncrease;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ---------- Text ---------- //
    public void changeTextMoney()
    {
        textMoney.text = " " + gameManager.money.ToString();
    }
}
