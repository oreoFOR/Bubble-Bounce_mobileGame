using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("StaticLevelNum", 1);
        if (PlayerPrefs.GetInt("StaticLevelNum") == 0)
        {
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("StaticLevelNum"));
    }
}
