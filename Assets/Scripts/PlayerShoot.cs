using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public LayerMask layerMask;
    public GameObject portalBullet;
    public float timeBetweenShots;

    private Vector3 portalTarget;
    private Vector3 shootingDirection;
    private RaycastHit2D ray;
    private GameObject bullet;
    private bool shooting = false;

    private void Start()
    {
        StartCoroutine(ShootPortal());
    }

    void Update ()
    {
        shootingDirection = Vector3.zero;

        if (Input.GetKey("up"))
        {
            shootingDirection.y = 1;
        }

        if (Input.GetKey("down"))
        {
            shootingDirection.y = -1;
        }

        if (Input.GetKey("right"))
        {
            shootingDirection.x = 1;
        }

        if (Input.GetKey("left"))
        {
            shootingDirection.x = -1;
        }

        Shoot();
    }

    private void Shoot()
    {
        if(shootingDirection != Vector3.zero)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
    }

    private IEnumerator ShootPortal()
    {
        while(true)
        {
            if(shooting)
            {
                bullet = Instantiate(portalBullet, transform.position + shootingDirection, Quaternion.identity);
                bullet.GetComponent<Portal>().moveDirection = shootingDirection;
            }
            else
            {
                //Debug.Log("Waiting");
            }

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
