using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("----- Associations -----")]
    public GameManager gameManager;
    public UIManager uiManager;

    [Header("----- Stats -----")]
    [SerializeField] float maxHP;   // 최대 체력
    [SerializeField] float hp;      // 현재 체력
    public float attackTime;  float currentTime;  // 공격 속도
    public int criticalChance;      // 치명타 확률
    public float criticalDamage;    // 치명타 dmg 계산
    public float bulletSpeed;       // bullet에서 접근하여 속도 계산
    public float bulletPower;       // 일반 dmg

    int enemyCount;
    bool isAttackReady = true;

    [Header(" ----- Components -----")]
    public GameObject bullet;
    public GameObject originPos;
    public AttackArea attackArea;

    [Header("----- Sounds ----- ")]
    public AudioSource attackSound;



    void Start()
    {
        init();
    }

    void init()
    {
        maxHP = hp = 30;        // 체력 초기값 설정
        attackTime = 3f;        // 공격 속도 초기값 설정
        bulletSpeed = 1f;       // 투사체 속도 초기값 설정
        bulletPower = 2f;       // 공격력 초기값 설정
        criticalChance = 0;     // 치명타 확률 초기값 설정
        criticalDamage = 1f;    // 치명타 데미지 초기값 설정
    }

    void Update()
    {
        // 타워가 포격 가능한 상태인지 체크
        checkAttackTimer();

        // 공격 가능한 상태일 때 enemyList에 Enemy가 있으면 doFire();
        if (isAttackReady && getEnemyListCount() > 0)
        {
            doFire();
        }
    }

    private void checkAttackTimer()
    {
        if (attackTime <= currentTime)
        {
            isAttackReady = true;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // 치명타를 고려한 bulletPower 계산
    public float calculateDamage()
    {
        float damage = bulletPower;

        // 치명타 확률이 0이면 그냥 bulletPower return
        if (criticalChance <= 0)
        {
            return damage;
        }

        // 랜덤 값을 이용해 치명타 적용 여부 판단
        bool isCritical = Random.Range(0, 100) < criticalChance;

        // 치명타인 경우 데미지 증가
        if (isCritical)
        {
            damage = bulletPower * criticalDamage;
        }

        return damage;
    }

    // 공격
    private void doFire()
    {
        // Bulelt 생성
        Instantiate(bullet, originPos.transform.position, originPos.transform.rotation);

        // 사운드 재생
        attackSound.Play();

        currentTime = 0;
        isAttackReady = false;
    }

    // Attack Area의 enemyList의 크기를 반환
    private int getEnemyListCount()
    {
        return attackArea.enemyList.Count;
    }

    public void plusHP(float plusValue)
    {
        hp += plusValue;
    }

    public void minusHP(float minusValue)
    {
        hp -= minusValue;

        // HP Bar UI에 변경된 체력 반영
        uiManager.checkHP(maxHP, hp);

        if (hp <=0)
        {
            gameManager.gameEnd();
        }
    }

    // 범위 내의 가장 가까운 enemy를 찾음
    public Vector2 findTarget()
    {
        // return값으로 사용할 벡터
        Vector2 resultVec = Vector2.zero;
        float nearDistance = Mathf.Infinity;

        foreach(GameObject enemy in attackArea.enemyList)
        {
            // enemy Positoin - Tower Position  // 리스트에 있는 enemy와의 거리를 구함
            Vector2 enemyDir = enemy.transform.position - originPos.transform.position;
            float distance = enemyDir.sqrMagnitude; // 제곱근 계산을 피하기 위해 제곱된 거리 사용

            // 더 작은 값을 return 값으로
            if (distance < nearDistance)
            {
                nearDistance = distance;
                resultVec = enemyDir;
                resultVec.Normalize();
            }
        }

        // 찾은 결과 return
        return resultVec;
    }
}
