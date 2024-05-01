using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public static int currentValue; // 引用Player的血量以更新
    public static int maxHealthValue;

    public TextMeshProUGUI healthText;

    private Image healthBarValue;

    void FillHealthValue()
    {
        if (maxHealthValue != 0)
        {
            healthBarValue.fillAmount = (float)currentValue / (float)maxHealthValue;
        }
    }

    void SetHealthText()
    {
        healthText.text = currentValue.ToString() + "/" + maxHealthValue.ToString();
    }

    void Start()
    {
        healthBarValue = GetComponent<Image>();
        currentValue = maxHealthValue;
    }

    void Update()
    {
        FillHealthValue();
        SetHealthText();
    }
}
