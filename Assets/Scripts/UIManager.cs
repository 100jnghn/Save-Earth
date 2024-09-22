using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Tower tower;

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

    // ----- 스탯 증감 값 배열 ----- //
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
    // 버튼 클릭 막는 함수 (최대값 도달)
    void buttonSetActiveFalse(Button button)
    {
        // 버튼 클릭 막음
        button.interactable = false;

        // 버튼 색상 변경
        // 현재 색상 가져옴
        ColorBlock colorBlock = button.colors;

        Color newColor = colorBlock.normalColor;
        newColor.a = 0.3f;

        colorBlock.normalColor = newColor;

        button.colors = colorBlock;
    }

    public void attackPowerUp()
    {
        tower.bulletPower += attackPowerArr[attackPowerPtr++];

        // 최대치 도달 -> 버튼 클릭 막는다
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
