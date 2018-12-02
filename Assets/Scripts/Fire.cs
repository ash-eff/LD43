using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private float timer = 2f;

	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Destroy(gameObject);
        }
	}
}
