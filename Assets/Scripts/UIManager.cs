using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("----- UI Components -----")]
    public Text textMoney;

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
