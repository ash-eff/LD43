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
    private Animator anim;
    private bool shooting = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootPortal());
    }

    void Update ()
    {
        shootingDirection = new Vector3(Input.GetAxisRaw("HorizontalShoot"), Input.GetAxisRaw("VerticalShoot"));

        Shoot();
    }

    private void Shoot()
    {
        if (shootingDirection != Vector3.zero)
        {
            shooting = true;

            float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
                anim.SetBool("Shoot", true);
                bullet = Instantiate(portalBullet, transform.position + shootingDirection, Quaternion.identity);
                bullet.GetComponent<Portal>().moveDirection = shootingDirection;
            }
            else
            {
                anim.SetBool("Shoot", false);
            }

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
