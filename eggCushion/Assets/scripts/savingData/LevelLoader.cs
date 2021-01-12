using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelLoader : MonoBehaviour
{
    public GameObject[] lvls;
    public GameObject[] stars;
    PlayerProfile profile;

    void Start()
    {
        profile = GetComponent<PlayerProfile>();
        SaveData data = SerializationManager.Load("playerProfile");
        if (data != null)
        {
            profile.starNum = data.starNum;
            int ln = PlayerPrefs.GetInt("StaticLevelNum");
            print(ln);
            for (int i = 0; i < ln; i++)
            {
                RectTransform rect = lvls[i].GetComponent<RectTransform>();
                print(data.starNum[i]);
                Instantiate(stars[data.starNum[i]], rect.position, Quaternion.identity, rect);
                lvls[i].SetActive(true);
            }
        }
        else
        {
            lvls[0].SetActive(true);
        }
    }
    public void LoadLevel(int lvlNum)
    {
        SceneManager.LoadScene(lvlNum + 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
