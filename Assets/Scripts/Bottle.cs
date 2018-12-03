using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {

    public Vector3 moveDirection;
    public GameObject fireIndicator;
    public GameObject fireArea;
    private GameObject indicator;
    private float speed = 9f;
    private bool isExploded = false;
    private SpriteRenderer spr;

    private void Start()
    {
        indicator = Instantiate(fireIndicator, moveDirection, Quaternion.identity) as GameObject;
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // instantiate indicator at moveDirection

        transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);

        if(transform.position.x == moveDirection.x && transform.position.y == moveDirection.y)
        {
            if (!isExploded)
            {
                Destroy(indicator);
                spr.enabled = false;
                Instantiate(fireArea, transform.position, Quaternion.identity);
                isExploded = true;
                Destroy(gameObject);
            }
        }
    }
}
