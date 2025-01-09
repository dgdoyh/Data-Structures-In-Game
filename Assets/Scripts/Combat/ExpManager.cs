using System;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    #region Singleton
    public static ExpManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private int level = 1;
    private int exp = 0;
    private int maxExp = 10;

    public event Action OnLevelUp;


    public void GainExp(int expToAdd)
    {
        exp += expToAdd;

        if (exp > maxExp)
        {
            exp -= maxExp;

            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        maxExp = maxExp * level;

        Debug.Log("Level Up!");

        OnLevelUp?.Invoke();
    }
}
