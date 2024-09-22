using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Tower tower;

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

    // ----- ���� ���� �� �迭 ----- //
    float[] attackPowerArr = { 0.2f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f}; 
    float[] attackSpeedArr = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f};

    int attackPowerPtr = 0;
    int attackSpeedPtr = 0;





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

    // ---------- button ---------- //
    // ��ư Ŭ�� ���� �Լ� (�ִ밪 ����)
    void buttonSetActiveFalse(Button button)
    {
        // ��ư Ŭ�� ����
        button.interactable = false;

        // ��ư ���� ����
        // ���� ���� ������
        ColorBlock colorBlock = button.colors;

        Color newColor = colorBlock.normalColor;
        newColor.a = 0.3f;

        colorBlock.normalColor = newColor;

        button.colors = colorBlock;
    }

    public void attackPowerUp()
    {
        tower.bulletPower += attackPowerArr[attackPowerPtr++];

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (attackPowerPtr >= attackPowerArr.Length)
        {
            buttonSetActiveFalse(btnAttackPower);
        }
    }

    public void attackSpeedUp()
    {
        tower.attackTime -= attackSpeedArr[attackSpeedPtr++];

        if (attackSpeedPtr >= attackSpeedArr.Length)
        {
            buttonSetActiveFalse(btnAttackSpeed);
        }
    }
}
