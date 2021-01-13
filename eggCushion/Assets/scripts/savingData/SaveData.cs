using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int levelNum;
    public List<int> starNum = new List<int>();
    public SaveData(PlayerProfile profile)
    {
        starNum = profile.starNum;
    }
}
