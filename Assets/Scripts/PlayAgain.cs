﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {

    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
