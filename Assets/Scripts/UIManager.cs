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

    [Header("----- UI Components Panels -----")]
    public GameObject panelAttack;
    public GameObject panelHP;
    public GameObject panelEnding;

    [Header("----- UI Components Text -----")]
    public Text txtGameScore;   // 진행중인 게임의 점수
    public Text txtHighScore;   // 개인 최고 점수
    public Text txtMyScore;     // 엔딩 화면에 띄울 게임 점수

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
    float[] attackPowerArr = { 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f };    // Length = 8
    int[] attackPowerMoney = { 3, 3, 3, 4, 4, 5, 6, 8 };

    // 공격 속도
    float[] attackSpeedArr = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f};   // Length = 13
    int[] attackSpeedMoney = { 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7, 8 };

    // 투사체 속도
    int[] bulletSpeedMoney = { 3, 3, 4, 5, 6, 7, 8, 9 };

    // 치명타 확률
    int[] criticalMoney = { 3, 3, 4, 4, 5, 6, 7, 8, 9, 10 };

    // 공격 범위
    float[] attackAreaArr = { 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.1f };
    int[] attackAreaMoney = { 3, 3, 4, 5, 6, 7 };

    // 스탯 index를 가르키는 포인터
    int attackPowerPtr = 0;
    int attackSpeedPtr = 0;
    int bulletSpeedPtr = 0; // max = 8 -> bulletSpeed는 Arr가 아니라 일정 값을 multiply한다
    int criticalChancePtr = 0;
    int criticalDamagePtr = 0;
    int attackAreaPtr = 0;

    // 스탯 증가값 변수
    float bulletSpeedMul = 1.3f;
    float criticalDamageMul = 1.1f;
    int criticalChanceAdd = 10;

    



    // ----- 게임 시작, 종료 시 UI 관리 ----- //
    public void gameStart()
    {
        panelEnding.SetActive(false);
    }

    public void gameEnd(int highScore, int myScore)
    {
        panelEnding.SetActive(true);
        txtHighScore.text = highScore.ToString();
        txtMyScore.text = myScore.ToString();
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
