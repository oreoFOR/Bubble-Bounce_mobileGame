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
    private void Start()
    {
        StartGame();
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
            PlayerPrefs.SetInt("StaticLevelNum", lvlNum + 1);
            if (bubblesBlown <= 6)
            {
                goldenEggs[1].SetActive(true);
            }
            if (!isCracked)
            {
                goldenEggs[2].SetActive(true);
            }
        }
    }
    public void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    public void NextLvl()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
