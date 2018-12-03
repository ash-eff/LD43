using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Signs : MonoBehaviour {

    public GameObject[] signs;
    public List<String> words =  new List<String>();
    private float timer;
    private bool running = true;

	// Use this for initialization
	void Start ()
    {
        timer = UnityEngine.Random.Range(5f, 7f);
        StartCoroutine(FlipSigns());
	}

    private void Update()
    {
        if(running)
        {
            timer -= Time.deltaTime;
        }


        if(timer < 0)
        {
            running = false;
            timer = timer = UnityEngine.Random.Range(5f, 12f);
            StartCoroutine(FlipSigns());
        }
    }

    IEnumerator FlipSigns()
    {
        float yRange = UnityEngine.Random.Range(-5.0f, 1.4f);
        String word = words[UnityEngine.Random.Range(0, words.Count)];
        int i = 0;
        while (!running)
        {
            foreach(GameObject sign in signs)
            {
                sign.GetComponentInChildren<TextMesh>().text = word[i].ToString();
                sign.transform.position = new Vector3(sign.transform.position.x, yRange, 0f); 
                sign.GetComponent<FlipSign>().flipUP = true;
                i++;
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(4f);

            foreach (GameObject sign in signs)
            {
                sign.GetComponent<FlipSign>().flipDown = true;
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(4f);

            running = true;
        }
    }
}
