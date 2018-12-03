using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHobo : MonoBehaviour {

    public GameObject blood;
    private float speed = 4f;
    private Rigidbody2D rb2d;
    private float rot;
    private float rotSpeed;
    private int select;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        select = Random.Range(0, 2);
        rotSpeed = Random.Range(.5f, 1);
        if(select == 0)
        {
            rot = -130;
        }
        else
        {
            rot = 130;
        }
    }

    private void Update()
    {
        rb2d.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(transform.rotation.eulerAngles.z, rot, Time.deltaTime * rotSpeed));
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
