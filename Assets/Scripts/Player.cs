using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath KillPlayer;

    public int lives = 3;
    public bool canBeDamaged = true;
    public float iFrameTime;
    public float timer;
    public GameObject[] life;

    private Collider2D coll;
    private Color tmp;

    public void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void Update()
    {
        if(lives <= 0)
        {
            PlayerDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDamaged && collision.gameObject.tag == "Hobo" || canBeDamaged && collision.gameObject.tag == "Fire")
        {
            canBeDamaged = false;
            coll.enabled = false;
            StartCoroutine(IFrames());
            lives -= 1;
            if(lives >= 0)
            {
                life[lives].SetActive(false);
            }
        }
    }

    void PlayerDead()
    {
        if (KillPlayer != null)
        {
            KillPlayer();
        }
    }

    IEnumerator IFrames()
    {
        timer = iFrameTime;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            

            yield return new WaitForEndOfFrame();
        }

        coll.enabled = true;
        canBeDamaged = true;
    }
}