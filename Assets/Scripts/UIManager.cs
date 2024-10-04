using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UI Component�� UI�� ��ɵ��� �����ϴ� ��ũ��Ʈ
public class UIManager : MonoBehaviour
{
    [Header ("----- Associations -----")]
    public GameManager gameManager;
    public Tower tower;
    public AttackArea attackArea;

    [Header ("----- UI Components Player -----")]
    public Text textMoney;  // Text - ���� money

    [Header("----- UI Components Panels -----")]
    public GameObject panelAttack;
    public GameObject panelHP;
    public GameObject panelEnding;

    [Header("----- UI Components Text -----")]
    public Text txtGameScore;   // �������� ������ ����
    public Text txtHighScore;   // ���� �ְ� ����
    public Text txtMyScore;     // ���� ȭ�鿡 ��� ���� ����

    [Header("----- UI Components Tab Buttons -----")]
    public Button btnAttackTab;
    public Button btnHPTab;

    [Header("----- UI Components Attack -----")]
    public Button btnAttackPower;       // btn - ���ݷ� ����
    public Button btnAttackSpeed;       // btn - ���� �ӵ� ����
    public Button btnAttackRange;       // btn - ���� ���� ����
    public Button btnBulletSpeed;       // btn - ����ü �ӵ� ����
    public Button btnCriticalChance;    // btn - ġ��Ÿ Ȯ�� ����
    public Button btnCriticalDamage;    // btn - ġ��Ÿ ������ ����

    [Header("----- UI Components Health -----")]
    public Button btnHPRecover;
    public Button btnHPIncrease;

    [Header("----- UI Components Stage -----")]
    public Text txtStage;
    public Text txtRemainTime;

    // ----- ���� ���� �� �迭 ----- //
    // ���ݷ�
    float[] attackPowerArr = { 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f };    // Length = 8
    int[] attackPowerMoney = { 3, 3, 3, 4, 4, 5, 6, 8 };

    // ���� �ӵ�
    float[] attackSpeedArr = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f};   // Length = 13
    int[] attackSpeedMoney = { 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7, 8 };

    // ����ü �ӵ�
    int[] bulletSpeedMoney = { 3, 3, 4, 5, 6, 7, 8, 9 };

    // ġ��Ÿ Ȯ��
    int[] criticalMoney = { 3, 3, 4, 4, 5, 6, 7, 8, 9, 10 };

    // ���� ����
    float[] attackAreaArr = { 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.1f };
    int[] attackAreaMoney = { 3, 3, 4, 5, 6, 7 };

    // ���� index�� ����Ű�� ������
    int attackPowerPtr = 0;
    int attackSpeedPtr = 0;
    int bulletSpeedPtr = 0; // max = 8 -> bulletSpeed�� Arr�� �ƴ϶� ���� ���� multiply�Ѵ�
    int criticalChancePtr = 0;
    int criticalDamagePtr = 0;
    int attackAreaPtr = 0;

    // ���� ������ ����
    float bulletSpeedMul = 1.3f;
    float criticalDamageMul = 1.1f;
    int criticalChanceAdd = 10;

    



    // ----- ���� ����, ���� �� UI ���� ----- //
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
    // Attack Tab Button�� ����
    public void onClickAttackTab()
    {
        // ���� ��ư�� �̹����� ���İ��� 1�� ���� -> �����ϰ� ���̵���
        Color selectedColor = btnAttackTab.image.color;
        selectedColor.a = 1f;
        btnAttackTab.image.color = selectedColor;

        // �ٸ� ��ư���� ���İ� ����
        Color unselectedColor = btnAttackTab.image.color;
        unselectedColor.a = 0.3f;
        btnHPTab.image.color = unselectedColor;

        

        // �ٸ� �гε� ��Ȱ��ȭ
        panelHP.SetActive(false);

        // ���� �г� Ȱ��ȭ
        panelAttack.SetActive(true);
    }

    // Health Tab Button�� ����
    public void onClickHealthTab()
    {
        // ���� ��ư�� �̹����� ���İ��� 1�� ���� -> �����ϰ� ���̵���
        Color selectedColor = btnHPTab.image.color;
        selectedColor.a = 1f;
        btnHPTab.image.color = selectedColor;

        // �ٸ� ��ư���� ���İ� ����
        Color unselectedColor = btnHPTab.image.color;
        unselectedColor.a = 0.3f;
        btnAttackTab.image.color = unselectedColor;



        // �ٸ� �гε� ��Ȱ��ȭ
        panelAttack.SetActive(false);

        // ü�� �г� Ȱ��ȭ
        panelHP.SetActive(true);
    }





    // ���׷��̵带 ���� money�� ������� üũ�ϴ� �Լ�
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

    // ���׷��̵� �� money ����
    void purchase(int value)
    {
        if (gameManager.money < value)
        {
            return;
        }



        // money ����
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

    // ������ -> stage up
    public void changeStage(int level)
    {
        txtStage.text = "Stage " + level.ToString();
    }

    // stage ���� �ð� ǥ��
    public void showRemainTime(float time)
    {
        time = (int)time;
        txtRemainTime.text = time.ToString() + " ";
    }




    // ---------- Attack Stat button ---------- //
    // ��ư Ŭ�� ���� �Լ� (�ִ밪 ����)
    void buttonDeactive(Button button)
    {
        // ��ư Ŭ�� ����
        button.interactable = false;

        // ��ư ���� ����

        // ���� ���� ������
        ColorBlock colorBlock = button.colors;

        // ���ο� ���� ����, ���� ������ ����
        Color newColor = colorBlock.normalColor;

        // ���ο� ���� alpha�� ����
        newColor.a = 0.3f;

        // ���ο� ������ ����
        colorBlock.normalColor = newColor;

        // ���� ����
        button.colors = colorBlock;
    }

    // ���ݷ� ���� ��ư
    public void attackPowerUp()
    {
        if (!checkMoney(attackPowerMoney[attackPowerPtr]))
        {
            return;
        }



        // tower�� ���ݷ� ����
        tower.bulletPower += attackPowerArr[attackPowerPtr];

        // gameManager�� money ����
        purchase(attackPowerMoney[attackPowerPtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (attackPowerPtr >= attackPowerArr.Length)
        {
            buttonDeactive(btnAttackPower);
        }
    }

    // ���� �ӵ� ���� ��ư
    public void attackSpeedUp()
    {
        if (!checkMoney(attackSpeedMoney[attackSpeedPtr]))
        {
            return;
        }



        // tower�� ���� �ӵ� ����
        tower.attackTime -= attackSpeedArr[attackSpeedPtr];

        // gameManager�� money ����
        purchase(attackSpeedMoney[attackSpeedPtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (attackSpeedPtr >= attackSpeedArr.Length)
        {
            buttonDeactive(btnAttackSpeed);
        }
    }

    // ����ü �ӵ� ���� ��ư
    public void bulletSpeedUp()
    {
        if (!checkMoney(bulletSpeedMoney[bulletSpeedPtr]))
        {
            return;
        }



        // ����ü �ӵ� ����
        tower.bulletSpeed *= bulletSpeedMul;

        // gameManager�� money ����
        purchase(bulletSpeedMoney[bulletSpeedPtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (bulletSpeedPtr >= bulletSpeedMoney.Length)
        {
            buttonDeactive(btnBulletSpeed);
        }
    }

    // ġ��Ÿ Ȯ�� ���� ��ư
    public void criticalChanceUp()
    {
        if (!checkMoney(criticalMoney[criticalChancePtr]))
        {
            return;
        }



        // ġ��Ÿ Ȯ�� ����
        tower.criticalChance += criticalChanceAdd;

        // gameManager�� money ����
        purchase(criticalMoney[criticalChancePtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (criticalChancePtr >= criticalMoney.Length)
        {
            buttonDeactive(btnCriticalChance);
        }
    }

    // ġ��Ÿ dmg ���� ��ư
    public void criticalDamageUp()
    {
        if (!checkMoney(criticalMoney[criticalDamagePtr]))
        {
            return;
        }



        // ġ��Ÿ ������ ����
        tower.criticalDamage *= criticalDamageMul;

        // gameManager�� money ����
        purchase(criticalMoney[criticalDamagePtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
        if (criticalDamagePtr >= criticalMoney.Length)
        {
            buttonDeactive(btnCriticalDamage);
        }
    }

    // ���� ���� ���� ��ư
    public void attackAreaUp()
    {
        if (!checkMoney(attackAreaMoney[attackAreaPtr]))
        {
            return;
        }



        // ���� ���� ����
        attackArea.expandArea(attackAreaArr[attackAreaPtr]);

        // gameManager�� money ����
        purchase(attackAreaMoney[attackAreaPtr++]);

        // ���ҵ� money�� UI�� �ݿ�
        changeTextMoney();

        // �ִ�ġ ���� -> ��ư Ŭ�� ���´�
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
        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
