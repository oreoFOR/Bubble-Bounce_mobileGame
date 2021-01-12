using System.Collections;
using UnityEngine;

public class BubbleBlower : MonoBehaviour
{
    //gameobj
    public GameObject bubble;
    //transform
    Transform currentBubble;
    public Transform blower;
    //ints
    int bubbleNum;
    int staticBubbleNum;
    public int maxBubbleNum;
    int size;
    //customs
    public GameManager gameManager;
    //public BubbleLoader loader;
    public Camera cam;
    public PhysicsMaterial2D[] bubbleMats;
    public LayerMask bubbleMask;
    //bools
    bool followFinger;
    //vectors
    public Vector3 maxBubbleSize;
    //floats
    public float popSize;
    public float growSpeed;
    public float mediumThreshold;
    public float largeThreshold;
    public  void SetBubbleNum(bool staticBubble)
    {
        bubbleNum -= 1;
        staticBubbleNum -= 1;
    }
    private void Update()
    {
        #region find/create bubble
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction, Mathf.Infinity, bubbleMask);
            if (hit)
            {
                SpawnBlower();
                currentBubble = hit.transform;
                followFinger = true;
            }
            else if (bubbleNum < maxBubbleNum)
            {
                followFinger = true;
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                currentBubble = Instantiate(bubble, mousePos, Quaternion.identity).transform;
                SpawnBlower();
                bubbleNum += 1;
                staticBubbleNum += 1;
                currentBubble.GetComponent<Bubble>().bubbleId = staticBubbleNum;
                gameManager.bubblesBlown = staticBubbleNum;
            }
            else
            {
                currentBubble = null;
            }
        }
        #endregion
        #region deselect bubble
        else if (Input.GetMouseButtonUp(0))
        {
            size = 0;
            EndBlower();
            if (currentBubble != null)
            {
                followFinger = false;
                if(currentBubble.localScale.x < 0.08f)
                {
                    Destroy(currentBubble.gameObject);
                    SetBubbleNum(true);
                }
            }
        }
        #endregion
        #region bubble growth
        if (followFinger)
        {
            if(currentBubble != null)
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                currentBubble.position = mousePos;
                blower.position = mousePos;
                currentBubble.localScale = Vector3.Lerp(currentBubble.localScale, maxBubbleSize, growSpeed * Time.deltaTime);
                if (currentBubble.localScale.x > popSize)
                {
                    followFinger = false;
                    Destroy(currentBubble.gameObject);
                    SetBubbleNum(true);
                    gameManager.bubblesBlown = bubbleNum;
                    EndBlower();
                }
            }
        }
        #endregion
        #region physics mats
        if(currentBubble != null)
        {
            if (currentBubble.localScale.x > largeThreshold && size < 2)
            {
                size = 2;
                currentBubble.GetComponent<CircleCollider2D>().sharedMaterial = bubbleMats[1];
            }
            else if (currentBubble.localScale.x > mediumThreshold && size < 1)
            {
                size = 1;
                currentBubble.GetComponent<CircleCollider2D>().sharedMaterial = bubbleMats[0];
            }
        }
        #endregion
    }
    void SpawnBlower()
    {
        blower.gameObject.SetActive(true);
    }
    void EndBlower()
    {
        if(blower != null)
        {
            blower.GetComponent<Animator>().SetTrigger("fade");
            print("have set trigger");
            StartCoroutine(DestroyBlower());
        }
    }
    IEnumerator DestroyBlower()
    {
        yield return new WaitForSeconds(0.5f);
        blower.gameObject.SetActive(false);
    }
}
