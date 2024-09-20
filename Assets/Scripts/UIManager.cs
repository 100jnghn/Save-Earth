using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header ("UI Components Player")]
    public Text textMoney;  // Text - ���� money

    [Header("UI Components Panels")]
    public GameObject panelAttack;
    public GameObject panelHP;

    [Header("UI Components Tab Buttons")]
    public Button btnAttack;
    public Button btnHP;

    [Header("----- UI Components Attack -----")]
    public Button btnAttackPower;       // btn - ���ݷ� ����
    public Button btnAttackSpeed;       // btn - ���� �ӵ� ����
    public Button btnAttackRange;       // btn - ���� ���� ����
    public Button btnBulletSpeed;       // btn - ����ü �ӵ� ����
    public Button btnCriticalChance;    // btn - ġ��Ÿ Ȯ�� ����
    public Button btnCriticalDamage;    // btn - ġ��Ÿ ������ ����

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
