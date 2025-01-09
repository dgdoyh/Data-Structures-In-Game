using System;
using System.Collections.Generic;


[Serializable]
public class GameData
{
    public Dictionary<string, float> data = new Dictionary<string, float>();

    public GameData()
    {
        data.Add("EnemyHp", 500);
    }

    public GameData(string key, float value)
    {
        data.Add(key, value);
    }
}
