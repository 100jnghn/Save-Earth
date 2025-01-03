using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UI Component와 UI의 기능들을 관리하는 스크립트
public class UIManager : MonoBehaviour
{
    [Header ("----- Associations -----")]
    public GameManager gameManager;
    public Tower tower;
    public AttackArea attackArea;

    [Header ("----- UI Components Player -----")]
    public Text textMoney;  // Text - 현재 money
    public Slider hpBar;    // HP Bar 슬라이더


    [Header("----- UI Components Panels -----")]
    public GameObject panelAttack;
    public GameObject panelHP;
    public GameObject panelEnding;

    [Header("----- UI Components Text -----")]
    public Text txtGameScore;   // 진행중인 게임의 점수
    public Text txtHighScore;   // 개인 최고 점수
    public Text txtMyScore;     // 엔딩 화면에 띄울 게임 점수

    public Text txtAttackPowerPay;  // 공격력 업그레이드를 위해 지불해야 하는 돈
    public Text txtAttackSpeedPay;
    public Text txtAttackRangePay;
    public Text txtBulletSpeedPay;
    public Text txtCriticalChancePay;
    public Text txtCriticalDamagePay;



    [Header("----- UI Components Tab Buttons -----")]
    public Button btnAttackTab;
    public Button btnHPTab;

    [Header("----- UI Components Attack -----")]
    public Button btnAttackPower;       // btn - 공격력 증가
    public Button btnAttackSpeed;       // btn - 공격 속도 증가
    public Button btnAttackRange;       // btn - 공격 범위 증가
    public Button btnBulletSpeed;       // btn - 투사체 속도 증가
    public Button btnCriticalChance;    // btn - 치명타 확률 증가
    public Button btnCriticalDamage;    // btn - 치명타 데미지 증가

    [Header("----- UI Components Health -----")]
    public Button btnHPRecover;
    public Button btnHPIncrease;

    [Header("----- UI Components Stage -----")]
    public Text txtStage;
    public Text txtRemainTime;

    // ----- 스탯 증감 값 배열 ----- //
    // 공격력
    float[] attackPowerArr = { 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 
                               0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };    // Length = 8
    int[] attackPowerMoney = { 02, 03, 04, 05, 06, 07, 08, 09, 10, 11,
                               13, 15, 17, 19, 20, 23, 27, 30, 35, 38, 42, 45 };

    // 공격 속도
    float[] attackSpeedArr = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f,
                               0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};   // Length = 13
    int[] attackSpeedMoney = { 01, 02, 03, 03, 04, 05, 06, 06, 07, 07,
                               08, 09, 10, 13, 17, 22, 25, 30, 35, 42 };

    // 투사체 속도
    int[] bulletSpeedMoney = { 02, 03, 04, 05, 07, 09, 11, 14, 17, 
                               20, 25, 30 };

    // 치명타 확률
    int[] criticalMoney = { 7, 10, 12, 15, 18, 23, 30, 40, 52, 70 };

    // 공격 범위
    float[] attackAreaArr = { 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.1f };
    int[] attackAreaMoney = { 5, 10, 15, 20, 30, 45, 60 };

    // 스탯 index를 가르키는 포인터
    int attackPowerPtr = 0;
    int attackSpeedPtr = 0;
    int bulletSpeedPtr = 0; // max = 8 -> bulletSpeed는 Arr가 아니라 일정 값을 multiply한다
    int criticalChancePtr = 0;
    int criticalDamagePtr = 0;
    int attackAreaPtr = 0;

    // 스탯 증가값 변수
    float bulletSpeedMul = 1.2f;
    float criticalDamageMul = 1.1f;
    int criticalChanceAdd = 10;






    // ----- 게임 시작, 종료 시 UI 관리 ----- //
    public void gameStart()
    {
        panelEnding.SetActive(false);

        // 업그레이드를 위한 돈 UI Text 초기화
        txtAttackPowerPay.text = attackPowerMoney[attackPowerPtr].ToString();
        txtAttackRangePay.text = attackAreaMoney[attackAreaPtr].ToString();
        txtAttackSpeedPay.text = attackSpeedMoney[attackSpeedPtr].ToString();
        txtBulletSpeedPay.text = bulletSpeedMoney[bulletSpeedPtr].ToString();
        txtCriticalChancePay.text = criticalMoney[criticalChancePtr].ToString();
        txtCriticalDamagePay.text = criticalMoney[criticalDamagePtr].ToString();

        // HP UI 초기화
        hpBar.value = 1;
    }

    public void gameEnd(int highScore, int myScore)
    {
        panelEnding.SetActive(true);
        txtHighScore.text = highScore.ToString();
        txtMyScore.text = myScore.ToString();
    }

    // ----- 체력 바 변경 ----- //
    public void checkHP(float maxHP, float hp)
    {
        hpBar.value = hp/maxHP;
    }

    // ----- Button Tab ----- //
    // Attack Tab Button을 누름
    public void onClickAttackTab()
    {
        // 누른 버튼의 이미지의 알파값을 1로 설정 -> 선명하게 보이도록
        Color selectedColor = btnAttackTab.image.color;
        selectedColor.a = 1f;
        btnAttackTab.image.color = selectedColor;

        // 다른 버튼들은 알파값 낮춤
        Color unselectedColor = btnAttackTab.image.color;
        unselectedColor.a = 0.3f;
        btnHPTab.image.color = unselectedColor;

        

        // 다른 패널들 비활성화
        panelHP.SetActive(false);

        // 공격 패널 활성화
        panelAttack.SetActive(true);
    }

    // Health Tab Button을 누름
    public void onClickHealthTab()
    {
        // 누른 버튼의 이미지의 알파값을 1로 설정 -> 선명하게 보이도록
        Color selectedColor = btnHPTab.image.color;
        selectedColor.a = 1f;
        btnHPTab.image.color = selectedColor;

        // 다른 버튼들은 알파값 낮춤
        Color unselectedColor = btnHPTab.image.color;
        unselectedColor.a = 0.3f;
        btnAttackTab.image.color = unselectedColor;



        // 다른 패널들 비활성화
        panelAttack.SetActive(false);

        // 체력 패널 활성화
        panelHP.SetActive(true);
    }





    // 업그레이드를 위한 money가 충분한지 체크하는 함수
    bool checkMoney(int compare)
    {
        if (gameManager.money >= compare)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 업그레이드 후 money 감소
    void purchase(int value)
    {
        if (gameManager.money < value)
        {
            return;
        }



        // money 차감
        gameManager.money -= value;
    }





    // ---------- Text ---------- //
    public void changeTextMoney()
    {
        textMoney.text = " " + gameManager.money.ToString();
    }
    public void changeTextMyScore(int score)
    {
        txtGameScore.text = score.ToString();
    }

    // 레벨업 -> stage up
    public void changeStage(int level)
    {
        txtStage.text = "Stage " + level.ToString();
    }

    // stage 남은 시간 표시
    public void showRemainTime(float time)
    {
        time = (int)time;
        txtRemainTime.text = time.ToString() + " ";
    }




    // ---------- Attack Stat button ---------- //
    // 버튼 클릭 막는 함수 (최대값 도달)
    void buttonDeactive(Button button)
    {
        // 버튼 클릭 막음
        button.interactable = false;

        // 버튼 색상 변경

        // 현재 색상 가져옴
        ColorBlock colorBlock = button.colors;

        // 새로운 색상 생성, 현재 색상을 대입
        Color newColor = colorBlock.normalColor;

        // 새로운 색상 alpha값 변경
        newColor.a = 0.3f;

        // 새로운 색상을 대입
        colorBlock.normalColor = newColor;

        // 색상 변경
        button.colors = colorBlock;
    }

    // 공격력 증가 버튼
    public void attackPowerUp()
    {
        if (!checkMoney(attackPowerMoney[attackPowerPtr]))
        {
            return;
        }



        // tower의 공격력 증가
        tower.bulletPower += attackPowerArr[attackPowerPtr];

        // gameManager의 money 감소
        purchase(attackPowerMoney[attackPowerPtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (attackPowerPtr >= attackPowerArr.Length)
        {
            buttonDeactive(btnAttackPower);
        }

        // Text Pay 변경
        txtAttackPowerPay.text = attackPowerMoney[attackPowerPtr].ToString();
    }

    // 공격 속도 증가 버튼
    public void attackSpeedUp()
    {
        if (!checkMoney(attackSpeedMoney[attackSpeedPtr]))
        {
            return;
        }



        // tower의 공격 속도 증가
        tower.attackTime -= attackSpeedArr[attackSpeedPtr];

        // gameManager의 money 감소
        purchase(attackSpeedMoney[attackSpeedPtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (attackSpeedPtr >= attackSpeedArr.Length)
        {
            buttonDeactive(btnAttackSpeed);
        }

        // Text Pay 변경
        txtAttackSpeedPay.text = attackSpeedMoney[attackSpeedPtr].ToString();
    }

    // 투사체 속도 증가 버튼
    public void bulletSpeedUp()
    {
        if (!checkMoney(bulletSpeedMoney[bulletSpeedPtr]))
        {
            return;
        }



        // 투사체 속도 증가
        tower.bulletSpeed *= bulletSpeedMul;

        // gameManager의 money 감소
        purchase(bulletSpeedMoney[bulletSpeedPtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (bulletSpeedPtr >= bulletSpeedMoney.Length)
        {
            buttonDeactive(btnBulletSpeed);
        }

        // Text Pay 변경
        txtBulletSpeedPay.text = bulletSpeedMoney[bulletSpeedPtr].ToString();
    }

    // 치명타 확률 증가 버튼
    public void criticalChanceUp()
    {
        if (!checkMoney(criticalMoney[criticalChancePtr]))
        {
            return;
        }



        // 치명타 확률 증가
        tower.criticalChance += criticalChanceAdd;

        // gameManager의 money 감소
        purchase(criticalMoney[criticalChancePtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (criticalChancePtr >= criticalMoney.Length)
        {
            buttonDeactive(btnCriticalChance);
        }

        // Text Pay 변경
        txtCriticalChancePay.text = criticalMoney[criticalChancePtr].ToString();
    }

    // 치명타 dmg 증가 버튼
    public void criticalDamageUp()
    {
        if (!checkMoney(criticalMoney[criticalDamagePtr]))
        {
            return;
        }



        // 치명타 데미지 증가
        tower.criticalDamage *= criticalDamageMul;

        // gameManager의 money 감소
        purchase(criticalMoney[criticalDamagePtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (criticalDamagePtr >= criticalMoney.Length)
        {
            buttonDeactive(btnCriticalDamage);
        }

        // Text Pay 변경
        txtCriticalDamagePay.text = criticalMoney[criticalDamagePtr].ToString();
    }

    // 공격 범위 증가 버튼
    public void attackAreaUp()
    {
        if (!checkMoney(attackAreaMoney[attackAreaPtr]))
        {
            return;
        }



        // 공격 범위 증가
        attackArea.expandArea(attackAreaArr[attackAreaPtr]);

        // gameManager의 money 감소
        purchase(attackAreaMoney[attackAreaPtr++]);

        // 감소된 money를 UI에 반영
        changeTextMoney();

        // 최대치 도달 -> 버튼 클릭 막는다
        if (attackAreaPtr >= attackAreaArr.Length)
        {
            buttonDeactive(btnAttackRange);
        }

        // Text Pay 변경
        txtAttackRangePay.text = attackAreaMoney[attackAreaPtr].ToString();
    }





    // ---------- Health Stat Button ---------- //
    public void healthRecover()
    {
        
    }

    // ----- Replay Button ----- //
    public void replay()
    {
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
