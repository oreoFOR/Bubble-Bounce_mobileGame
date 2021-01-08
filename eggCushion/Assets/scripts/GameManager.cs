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
    //objs
    public GameObject confettiPrefab;
    public GameObject[] goldenEggs;
    public GameObject GameOverPanel;
    public GameObject eggPrefab;
    GameObject egg;
    //transforms
    public Transform eggSpawnPos;
    //int
    public int bubblesBlown;
    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        GetComponent<BubbleLoader>().LoadBubbles();
        blower.SetBubbleNum();
        egg = Instantiate(eggPrefab, eggSpawnPos.position, Quaternion.identity);
        Egg _egg = egg.GetComponent<Egg>();
        _egg.gameManager = this;
        _egg.cam = cam;
    }
    public void StartPhysics()
    {
        eggRb = egg.GetComponent<Rigidbody2D>();
        eggRb.bodyType = RigidbodyType2D.Dynamic;
        eggRb.gravityScale = 0.2f;
        clawAnim.SetTrigger("release");
    }
    public void GameEnd(bool isCracked, bool failed)
    {
        StartCoroutine(ActivateGamePanel());
        if (!failed) 
        {
            Instantiate(confettiPrefab, transform.position, Quaternion.identity);
            goldenEggs[0].SetActive(true);
            if (bubblesBlown <= 3)
            {
                goldenEggs[1].SetActive(true);
            }
            if (!isCracked)
            {
                goldenEggs[2].SetActive(true);
            }
        }
    }
    IEnumerator ActivateGamePanel()
    {
        yield return new WaitForSeconds(1.5f);
        GameOverPanel.SetActive(true);
    }
    IEnumerator DeactivateGamePanel()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        PlayerPrefs.SetInt("replay", 1);
        GetComponent<BubbleLoader>().SaveBubbles();
        StartCoroutine(DeactivateGamePanel());
    }
    public void NextLvl()
    {
        PlayerPrefs.SetInt("replay", 0);
    }
}
