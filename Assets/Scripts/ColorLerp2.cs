using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorLerp2 : MonoBehaviour
{
    public Color A = Color.magenta;
    public Color B = Color.blue;
    public float speed = 1.0f;

    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        img.color = Color.Lerp(A, B, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
