﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public BubbleBlower blower;
    Rigidbody2D rb;
    public float gravityScale = 0.35f;
    public float fireSpeed;
    public enum BouncerType
    {
        faller,
        firer,
        rotater
    }
    public BouncerType type;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(type != BouncerType.rotater)
        {
            if (collision.gameObject.CompareTag("Bubble"))
            {
                Destroy(collision.gameObject);
                blower.SetBubbleNum(false);
            }
        }
    }
    public void StartPhysics()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
        if(type == BouncerType.firer)
        {
            rb.AddForce(transform.right * fireSpeed);
        }
    }
}
