using System.Collections.Generic;
using UnityEngine;

public class BubbleBlower : MonoBehaviour
{
    //gameobj
    public GameObject bubble;
    //transform
    Transform currentBubble;
    //ints
    int bubbleNum;
    int staticBubbleNum;
    public int maxBubbleNum;
    int size;
    //customs
    public GameManager gameManager;
    public BubbleLoader loader;
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
    public  void SetBubbleNum()
    {
        List<bool> spawn = loader.layout.spawn;
        for (int i = 0; i < spawn.Count; i++)
        {
            if (spawn[i] == true)
            {
                staticBubbleNum += 1;
            }
        }
        bubbleNum = staticBubbleNum;
    }
    private void Update()
    {
        #region find/create bubble
        if (Input.GetMouseButtonDown(0))
        {
            print("mouse down");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction, Mathf.Infinity, bubbleMask);
            if (hit)
            {
                print("hit");
                currentBubble = hit.transform;
                followFinger = true;
            }
            else if (bubbleNum < maxBubbleNum)
            {
                print("not hit");
                followFinger = true;
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                currentBubble = Instantiate(bubble, mousePos, Quaternion.identity).transform;
                bubbleNum += 1;
                staticBubbleNum += 1;
                currentBubble.GetComponent<Bubble>().bubbleId = staticBubbleNum;
                gameManager.bubblesBlown = bubbleNum;
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
            if(currentBubble != null)
            {
                followFinger = false;
                loader.AddBubble(currentBubble.position, currentBubble.localScale);
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
                currentBubble.localScale = Vector3.Lerp(currentBubble.localScale, maxBubbleSize, growSpeed * Time.deltaTime);
                if (currentBubble.localScale.x > popSize)
                {
                    followFinger = false;
                    Destroy(currentBubble.gameObject);
                    bubbleNum -= 1;
                    gameManager.bubblesBlown = bubbleNum;
                    loader.RemoveBubble(currentBubble.GetComponent<Bubble>().bubbleId);
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
}
