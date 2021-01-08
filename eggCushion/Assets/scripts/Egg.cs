using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    //custom
    Rigidbody2D rb;
    public GameManager gameManager;
    public PhysicsMaterial2D eggMat;
    public LayerMask clawMask;
    public Camera cam;
    public enum EggGoal
    {
        saucepan,
        fryingPan
    }
    public EggGoal eggGoal = EggGoal.saucepan;
    //sprite stuff
    public SpriteRenderer eggSprite;
    public Sprite angryCrackedEggSprite;
    public Sprite scaredCrackedEggSprite;
    public Sprite scaredEggSprite;
    public Sprite happyEggSprite;
    //gameObjs
    public GameObject brokenEgg;
    public GameObject waterSplash;
    //float
    public float breakVel;
    public float crackVel;
    public float speed;
    //int
    //transform
    
    //bool
    bool isCracked;
    bool onSurface;
    bool hasStopped;
    bool movingFast;
    bool gameOver;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #region collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameOver)
        {
            onSurface = true;
            if (!collision.gameObject.CompareTag("Bubble"))
            {
                if (collision.relativeVelocity.magnitude > breakVel)
                {
                    ContactPoint2D contact = collision.contacts[0];
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                    Vector3 position = contact.point;
                    EggBreak(position, rotation);
                }
                else if (collision.relativeVelocity.magnitude > crackVel)
                {
                    if (!isCracked)
                    {
                        isCracked = true;
                        eggSprite.sprite = angryCrackedEggSprite;
                    }
                    else
                    {
                        ContactPoint2D contact = collision.contacts[0];
                        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                        Vector3 position = contact.point;
                        EggBreak(position, rotation);
                    }
                }
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onSurface = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saucepan"))
        {
            rb.drag = 5;
            gameManager.GameEnd(isCracked, false);
            Instantiate(waterSplash, transform.position, Quaternion.identity);
        }
    }
    #endregion
    #region update calls
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, clawMask);
            if (hit)
            {
                gameManager.StartPhysics();
            }
        }
    }
    private void FixedUpdate()
    {
        speed = rb.velocity.magnitude;
        if (rb.velocity.magnitude < 0.1f && onSurface && !hasStopped)
        {
            hasStopped = true;
            StartCoroutine(Stationary());
        }
        #region egg face
        if (rb.velocity.magnitude > 1f && !movingFast)
        {
            movingFast = true;
            if (isCracked)
            {
                eggSprite.sprite = scaredCrackedEggSprite;
            }
            else
            {
                eggSprite.sprite = scaredEggSprite;
            }
        }
        else if (rb.velocity.magnitude < 1f && movingFast)
        {
            movingFast = false;
            if (isCracked)
            {
                eggSprite.sprite = angryCrackedEggSprite;
            }
            else
            {
                eggSprite.sprite = happyEggSprite;
            }
        }
        #endregion
    }
    #endregion
    IEnumerator Stationary()
    {
        yield return new WaitForSeconds(0.75f);
        if (rb.velocity.magnitude < 0.1f && onSurface)
        {
            hasStopped = true;
            gameManager.GameEnd(isCracked, true);
            gameOver = true;
        }
        else
        {
            hasStopped = false;
        }
    }
    void EggBreak(Vector3 pos,Quaternion rot)
    {
        if(eggGoal == EggGoal.saucepan)
        {
            Instantiate(brokenEgg, pos, rot);
            gameManager.GameEnd(isCracked, true);
            gameOver = true;
            Destroy(gameObject);
        }
    }
}
