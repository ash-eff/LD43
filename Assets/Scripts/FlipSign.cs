using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSign : MonoBehaviour {

    public bool flipUP = false;
    public bool flipDown = false;

	
	// Update is called once per frame
	void Update ()
    {
        if (flipUP)
        {
            if(transform.rotation.x != 0)
            {
                transform.Rotate(Vector3.right * -180 * Time.deltaTime);
                if(transform.rotation.x <= 0)
                {
                    Quaternion x = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = x;
                    flipUP = false;
                }
            }
        }

        if (flipDown)
        {
            transform.Rotate(Vector3.right * 180 * Time.deltaTime);

            // this is stupid but it works
            if (transform.rotation.x >= .7f)
            {
                Quaternion x = Quaternion.Euler(90f, 0f, 0f);
                transform.rotation = x;
                flipDown = false;
            }
        }
	}
}
