using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int levelNum;
    public int[] starNum = new int[10];
    public SaveData(PlayerProfile profile)
    {
        starNum = profile.starNum;
    }
}
