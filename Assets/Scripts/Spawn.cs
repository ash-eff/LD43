using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    private Collider2D col;
    private Bounds bounds;
    public Vector3 spawnLoc;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        bounds = col.bounds;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        spawnLoc = new Vector3(Random.Range(-bounds.extents.x + transform.position.x, bounds.extents.x + transform.position.x), (Random.Range(-bounds.extents.y + transform.position.y, bounds.extents.y + transform.position.y)));
        Instantiate(enemy, spawnLoc, Quaternion.identity);
    }
}
