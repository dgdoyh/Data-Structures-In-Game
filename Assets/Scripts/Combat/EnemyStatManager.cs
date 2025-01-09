using UnityEngine;


public class EnemyStatManager : MonoBehaviour
{
    BinarySearchTree enemyPowerList = new BinarySearchTree();
    public int CurrEnemyPower { get; private set; }

    private void Start()
    {
        ExpManager.Singleton.OnLevelUp += UpdateEnemyPower;

        FillEnemyPowerList();
        CurrEnemyPower = enemyPowerList.GetMinValue();

        Debug.Log("Current Enemy Power: " + CurrEnemyPower);
    }

    private void OnDisable()
    {
        ExpManager.Singleton.OnLevelUp -= UpdateEnemyPower;
    }

    private void FillEnemyPowerList()
    {
        enemyPowerList.Insert(5);
        enemyPowerList.Insert(2);
        enemyPowerList.Insert(21);
        enemyPowerList.Insert(8);
        enemyPowerList.Insert(3);
        enemyPowerList.Insert(1);
        enemyPowerList.Insert(13);
    }

    private void UpdateEnemyPower()
    {
        enemyPowerList.Remove(CurrEnemyPower);
        CurrEnemyPower = enemyPowerList.GetMinValue();

        Debug.Log("Current Enemy Power: " + CurrEnemyPower);
    }
}
