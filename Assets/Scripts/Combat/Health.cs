using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;

    private float currHP;

    public float MaxHP { get => maxHP; }
    public float CurrHP { get => currHP; set => currHP = value; }

    // If it's true, it blocks attacks
    private bool isInvunerable;

    public event Action<int> OnTakeDamage;
    public event Action OnDie;

    // IsDead is true when currHP is 0
    public bool IsDead => currHP == 0;


    private void Awake()
    {
        currHP = maxHP;

        OnDie += RestoreHp;
    }

    private void OnDisable()
    {
        OnDie -= RestoreHp;
    }

    // This will be used in PlayerDodgeState and PlayerBlockState
    public void SetInvunerable(bool isInvunerable)
    {
        this.isInvunerable = isInvunerable;
    }

    public void GetDamage(int damage)
    {
        if (currHP == 0) { return; }

        // If it's invunerable, do not get damage
        if (isInvunerable) { return; }

        // Get damage, but currHP doesn't go down below 0
        currHP = Mathf.Max(currHP - damage, 0);

        OnTakeDamage?.Invoke(damage);

        if (currHP == 0)
        {
            OnDie?.Invoke();
        }

        ExpManager.Singleton.GainExp(10);
    }

    public void RestoreHp()
    {
        currHP = maxHP;
    }
}
