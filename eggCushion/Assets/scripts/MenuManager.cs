using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("StaticLevelNum") == 0)
        {
            PlayerPrefs.SetInt("StaticLevelNum", 2);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("StaticLevelNum"));
    }
    public void LvlSlct()
    {
        SceneManager.LoadScene(1);
    }
}
