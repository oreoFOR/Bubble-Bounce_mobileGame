using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //custom
    Rigidbody2D eggRb;
    public Animator clawAnim;
    public BubbleBlower blower;
    public Camera cam;
    public Claw claw;
    Egg _egg;
    public Bouncer[] physicsObjs;
    PlayerProfile profile;
    //objs
    public GameObject confettiPrefab;
    public GameObject[] goldenEggs;
    public GameObject GameOverPanel;
    public GameObject eggPrefab;
    public GameObject nxtLvl;
    GameObject egg;
    //transforms
    public Transform clawTarget;
    //int
    public int bubblesBlown;
    int starNum;
    int buildIndex;
    private void Start()
    {
        //ResetPP();
        Scene scene = SceneManager.GetActiveScene();
        buildIndex = scene.buildIndex;
        profile = GetComponent<PlayerProfile>();
        StartGame();
    }
    void ResetPP()
    {
        PlayerPrefs.SetInt("StaticLevelNum", 0);
        //PlayerProfile p = new PlayerProfile();
       //SerializationManager.Save("playerProfile", p);
    }
    void StartGame()
    {
        //GetComponent<BubbleLoader>().LoadBubbles();
        //blower.SetBubbleNum();
        egg = Instantiate(eggPrefab, clawTarget.position, Quaternion.identity);
        egg.transform.SetParent(clawTarget);
        _egg = egg.GetComponent<Egg>();
        _egg.gameManager = this;
        _egg.cam = cam;
        _egg.blower = blower;
    }
    public void StartPhysics()
    {
        egg.transform.SetParent(null);
        eggRb = egg.GetComponent<Rigidbody2D>();
        eggRb.bodyType = RigidbodyType2D.Dynamic;
        eggRb.gravityScale = 0.2f;
        claw.clawMode = Claw.ClawMode.leave;
        clawAnim.SetTrigger("release");
        _egg.StartTrail();
        if(physicsObjs!= null)
        {
            for (int i = 0; i < physicsObjs.Length; i++)
            {
                physicsObjs[i].StartPhysics();
            }
        }
    }
    public void GameEnd(bool isCracked, bool failed)
    {
        GameOverPanel.SetActive(true);
        if (!failed) 
        {
            nxtLvl.SetActive(true);
            Instantiate(confettiPrefab, transform.position, Quaternion.identity);
            goldenEggs[0].SetActive(true);
            int lvlNum = PlayerPrefs.GetInt("StaticLevelNum");
            if (buildIndex > lvlNum)
            {
                lvlNum += 1;
                PlayerPrefs.SetInt("StaticLevelNum", lvlNum);
            }
            starNum += 1;
            if (bubblesBlown <= 5)
            {
                goldenEggs[1].SetActive(true);
                starNum += 1;
            }
            if (!isCracked)
            {
                goldenEggs[2].SetActive(true);
                starNum += 1;
            }
        }
        SaveManage(starNum);

    }
    void SaveManage(int _starNum)
    {
        SaveData data = SerializationManager.Load("playerProfile");
        if(data != null)
        {
            profile.starNum = data.starNum;
            if (_starNum > data.starNum[buildIndex -2])
            {
                profile.starNum[buildIndex - 2] = _starNum;// -1 coz there is lvlSelect scene as buoldindex 1 not lvl 1
                SerializationManager.Save("playerProfile", profile);
            }
        }
        else
        {
            profile.starNum[buildIndex - 2] = _starNum;// -1 coz there is lvlSelect scene as buoldindex 1 not lvl 1
            SerializationManager.Save("playerProfile", profile);
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void NextLvl()
    {
        SceneManager.LoadScene(buildIndex + 1);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
