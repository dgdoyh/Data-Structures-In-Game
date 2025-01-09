using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image hpBarBackground;
    [SerializeField] Image hpBar;

    void Start()
    {
        health.OnTakeDamage += UpdateHPBar;
        health.OnDie += TurnOffHPBar;

        // When the game starts, it starts with Max HP
        // Parameter doesn't do anything
        UpdateHPBar(0);
    }

    // Parameter doesn't do anything since the damage will affect to CurrHP in Health
    public void UpdateHPBar(int damage)
    {
        hpBar.fillAmount = health.CurrHP / health.MaxHP;
    }

    private void TurnOffHPBar()
    {
        hpBarBackground.enabled = false;
    }
}
