using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {

    public Vector3 moveDirection;
    private float speed = 9f;
    private bool isExploded = false;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);

        if(transform.position.x == moveDirection.x && transform.position.y == moveDirection.y)
        {
            if (!isExploded)
            {
                isExploded = true;
                StartCoroutine(Explode());
            }
        }
    }

    IEnumerator Explode()
    {
        float timer = 3f;

        while (timer > 0)
        {
            print("Burn");
            timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
