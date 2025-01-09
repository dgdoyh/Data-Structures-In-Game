using UnityEngine;


public class GameDataManager : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;


    private void Awake()
    {
        GameData loadedData = SavingSystem.Load();

        enemyHealth.CurrHP = loadedData.data["EnemyHp"];
    }

    private void OnDisable()
    {
        SavingSystem.Save("EnemyHp", enemyHealth.CurrHP);
    }
}
