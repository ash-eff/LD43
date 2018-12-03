using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath KillPlayer;

    public int lives = 3;
    public bool canBeDamaged = true;
    public float iFrameTime;
    public float timer;
    public GameObject[] life;
    public GameObject dmgIndicator;
    public Color A = Color.magenta;
    public Color B = Color.blue;
    public float speed = 1.0f;

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
        StartCoroutine(DamageFlash());
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        coll.enabled = true;
        canBeDamaged = true;
    }

    IEnumerator DamageFlash()
    {
        float alpha = dmgIndicator.GetComponent<Image>().color.a;
        Color color = dmgIndicator.GetComponent<Image>().color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2)
        {
            Color newColor = new Color(color.r, color.b, color.g, Mathf.Lerp(1, 0, t));
            dmgIndicator.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}