using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    private Rigidbody2D rb2d;
    private GameObject target;
    private float speed = 6f;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
}
