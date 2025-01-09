using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] DamageText damageTextPrefab;

    private void Start()
    {
        health.OnTakeDamage += Spawn;
    }

    public void Spawn(int damage)
    {
        DamageText _damageText = Instantiate(damageTextPrefab, transform);
        _damageText.UpdateDamageText(damage);
    }
}
