using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class ColorLerp : MonoBehaviour
{
    public Color A = Color.magenta;
    public Color B = Color.blue;
    public float speed = 1.0f;

    Tilemap tm;

    void Start()
    {
        tm = GetComponent<Tilemap>();
    }

    void Update()
    {
        tm.color = Color.Lerp(A, B, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
