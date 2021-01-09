using UnityEngine;

public class Claw : MonoBehaviour
{
    public Transform eggSpawnPos;
    public Transform exitPoint;
    public float moveSpeed;
    public enum ClawMode
    {
        enter,
        stationary,
        leave
    }
    public ClawMode clawMode;
    private void Start()
    {
        transform.position = exitPoint.position;
    }
    private void Update()
    {
        if (clawMode == ClawMode.enter)
        {
            transform.position = Vector2.MoveTowards(transform.position, eggSpawnPos.position,moveSpeed * Time.deltaTime);
            if(transform.position == eggSpawnPos.position)
            {
                clawMode = ClawMode.stationary;
            }
        }
        else if (clawMode == ClawMode.leave)
        {
            transform.position = Vector2.MoveTowards(transform.position, exitPoint.position,moveSpeed *Time.deltaTime);
            if (transform.position == exitPoint.position)
            {
                clawMode = ClawMode.stationary;
            }
        }
    }
}
