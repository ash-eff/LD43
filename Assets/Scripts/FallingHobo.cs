﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHobo : MonoBehaviour {

    public GameObject blood;
    private float speed = 4f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Grinder")
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
