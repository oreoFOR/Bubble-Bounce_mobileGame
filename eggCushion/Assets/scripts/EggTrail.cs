using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggTrail : MonoBehaviour
{
    float distanceTravelled = 0;
    Vector3 lastPosition;
    public float dotSpawnDistance;
    public GameObject dot;
    public GameObject cloud;
    void Start()
    {
        lastPosition = transform.position;
    }
    void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        if(distanceTravelled > dotSpawnDistance)
        {
            SpawnDot(0);
        }
    }
    public void SpawnDot(int obj)
    {
        distanceTravelled = 0;
        if (obj == 0)
        {
            Instantiate(dot, transform.position, Quaternion.identity);
        }
        else if(obj == 1)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }
}
