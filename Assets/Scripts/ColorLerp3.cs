using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorLerp3 : MonoBehaviour
{
    public Color A = Color.magenta;
    public Color B = Color.blue;
    public float speed = 1.0f;

    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        txt.color = Color.Lerp(A, B, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
