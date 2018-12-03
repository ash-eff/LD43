using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float disAmount = 0.1f;
    public enum EnemyType { MELEE, RUNNER, RANGED}
    public EnemyType enemyType;
    public GameObject bottle;

    private Rigidbody2D rb2d;
    private GameObject target;
    private float speed;
    private bool canMove = true;
    private Vector3 direction;
    private Vector3 newDirection; 
    public float minSpeed;
    public float maxSpeed;
    private float runnerTimer = 3f;
    private Collider2D coll;
    private Renderer mat;
    private Animator anim;

    private void OnEnable()
    {
        Player.KillPlayer += PlayerDead;
    }

    private void OnDisable()
    {
        Player.KillPlayer -= PlayerDead;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        mat = GetComponent<Renderer>();
        speed = Random.Range(minSpeed, maxSpeed);
        rb2d = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().gameObject;
        direction = CheckPosition();

        if (enemyType == EnemyType.RANGED)
        {
            StartCoroutine(StaggeredMovement());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        runnerTimer -= Time.deltaTime;
        if(canMove)
        {
            float step = speed * Time.deltaTime;

            if(enemyType == EnemyType.MELEE)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxSpeed);
            }
           
            if(enemyType == EnemyType.RUNNER)
            {
                transform.position += direction * step;
                Vector3 vectorToTarget = direction - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxSpeed);
                if (runnerTimer <= 0)
                {
                    FindObjectOfType<GameManager>().NumToKill = 1;
                    Destroy(gameObject);          
                }
            }
        }
    }

    private Vector3 CheckPosition()
    {
        Vector3 dir;

        if(transform.position.x > 5.5 || transform.position.x < -.5)
        {
            if(transform.position.x > 5.5)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.right;
            }
        }
        else if (transform.position.y > 3 || transform.position.y < -3)
        {
            if (transform.position.y > 3)
            {
                dir = Vector3.down;
            }
            else
            {
                dir = Vector3.up;
            }
        }
        else
        {
            dir = Vector3.zero;
        }

        return dir;
    }

    IEnumerator StaggeredMovement()
    {
        anim.SetBool("walking", true);
        float timer = .5f;
        while(timer > 0)
        {
            
            timer -= Time.deltaTime;
            transform.position += direction * speed * Time.deltaTime;
            Vector3 vectorToTarget = direction - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxSpeed);
            yield return new WaitForEndOfFrame();
        }

        timer = Random.Range(.5f, 1f);
        
        if (direction.x == 1 || direction.x == -1)
        {
            int i  = Random.Range(0, 2);
            if(i == 0)
            {
                newDirection = new Vector3(0, 1);
            }
            else
            {
                newDirection = new Vector3(0, -1);
            } 
        }

        if (direction.y == 1 || direction.y == -1)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                newDirection = new Vector3(1, 0);
            }
            else
            {
                newDirection = new Vector3(-1, 0);
            }
        }

        while (canMove && timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position += newDirection * speed * Time.deltaTime;
            Vector3 vectorToTarget = newDirection;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxSpeed);
            yield return new WaitForEndOfFrame();
        }

        canMove = false;
        StartCoroutine(ThrowBottles());
    }

    IEnumerator ThrowBottles()
    {
        anim.SetBool("walking", false);
        StartCoroutine(FaceTarget());
        while (true)
        {
            anim.SetBool("throwing", true);
            bottle.GetComponent<Bottle>().moveDirection = target.transform.position;
            GameObject beerBottle = Instantiate(bottle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.5f);
            anim.SetBool("throwing", false);
            yield return new WaitForSeconds(3.5f);
        }
    }

    IEnumerator FaceTarget()
    {
        while (true)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxSpeed);

            yield return new WaitForEndOfFrame();
        }
    }

    public void EnemyDeath()
    {
        speed = 0;
        coll.enabled = false;
        StartCoroutine(KillHobo());
    }

    IEnumerator KillHobo()
    {
        disAmount = 0.1f;
        while (disAmount < 1f)
        {
            print("dissolve");
            disAmount += 0.1f;
            mat.material.SetFloat("_Level", disAmount);
            yield return new WaitForSeconds(.05f);
        }

        GameManager gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gm.SendMessage("MeatGrinder");
        int score = Random.Range(10, 26);
        gm.ScoreUpdate += score;
        Destroy(gameObject);
    }

    void PlayerDead()
    {
        canMove = false;
    }
}
