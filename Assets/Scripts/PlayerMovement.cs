using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb2d;
    private Vector3 moveDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        moveDirection = Vector2.zero;
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if(moveDirection != Vector3.zero)
        {
            Move();
        }
	}

    private void Move()
    {
        rb2d.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
    }
}
