using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private Rigidbody2D rb2d;
    public Vector3 moveDirection;
    private float speed = 20f;
    private float timer = 3f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
        timer -= Time.deltaTime;

        if(timer <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            if(collision.gameObject.tag == "Hobo")
            {
                collision.gameObject.GetComponent<EnemyMove>().EnemyDeath();
                GameManager gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                gm.EnemiesKilled = 1;
                gm.NumToKill = 1;
            }

            Destroy(gameObject);
        }
    }
}
