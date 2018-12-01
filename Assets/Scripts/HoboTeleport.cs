using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboTeleport : MonoBehaviour {

	public void Teleport()
    {
        transform.position = new Vector3(-8.0f, 4.0f, 0.0f);
    }
}
