﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartHobo : MonoBehaviour {

    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
