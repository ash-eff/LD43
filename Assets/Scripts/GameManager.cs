using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum State { SPAWNING, WAITING, PAUSE, STOP}
    public State state;

    public GameObject waveText;
    public GameObject gameOverPanel;
    public GameObject fallingHoboSpawner;
    public GameObject fallingHobo;
    public GameObject[] hobos;
    public GameObject[] hoboSpawners;
    public int enemiesKilled = 0;
    public int wave = 0;

    public int numToKill;
    private int numSpawning;
    public int baseNumSpawning;
    public int spawnIndex1;
    public int spawnIndex2;

    public float timer = 2;
    private float resetTimer;

    private void OnEnable()
    {
        Player.KillPlayer += PlayerDead;
    }

    private void OnDisable()
    {
        Player.KillPlayer -= PlayerDead;
    }

    public int EnemiesKilled
    {
        set { enemiesKilled += value; }
    }

    public int NumToKill
    {
        set { numToKill -= value; }
    }


    private void Start()
    {
        state = State.PAUSE;
        resetTimer = timer;
        SwitchWave();
        SetSpawnPoints();
        StartCoroutine(CountDown());
    }

    private void PlayerDead()
    {
        state = State.STOP;
        gameOverPanel.SetActive(true);
    }

    private void SwitchWave()
    {
        wave += 1;
    }

    private void SetSpawnPoints()
    {
        spawnIndex1 = Random.Range(0, 4);
        spawnIndex2 = Random.Range(0, 4);
        numSpawning = baseNumSpawning * wave;
        numToKill = numSpawning;
    }

    public void MeatGrinder()
    {
        Instantiate(fallingHobo, new Vector3(Random.Range(-41f, -39f), fallingHoboSpawner.transform.position.y, 0.0f), Quaternion.identity);
    }

    IEnumerator CountDown()
    {
        if(state != State.STOP)
        {
            waveText.GetComponent<WaveIndicator>().WaveUpdate(wave);
            float countDownTimer = 3f;
            while (countDownTimer > 0)
            {
                countDownTimer -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            state = State.SPAWNING;
            StartCoroutine(RunGame());
        }
    }

    IEnumerator RunGame()
    {
        while(state != State.PAUSE)
        {
            if(state == State.SPAWNING)
            {
                // TODO create a function that handles spawning chances based on wave number
                int hoboIndex = Random.Range(0, 3);
                hoboSpawners[spawnIndex1].GetComponent<Spawn>().SpawnEnemy(hobos[hoboIndex]);
                hoboIndex = Random.Range(0, 3);
                hoboSpawners[spawnIndex2].GetComponent<Spawn>().SpawnEnemy(hobos[hoboIndex]);
                numSpawning -= 2;

                if(numSpawning <= 0)
                {
                    state = State.WAITING;
                }

                yield return new WaitForSeconds(.5f);
            }
            else if(state == State.WAITING)
            {
                Debug.Log("Wait");
                if (numToKill <= 0 && numSpawning <= 0)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        timer = resetTimer;
                        SwitchWave();
                        SetSpawnPoints();
                        state = State.PAUSE;
                    }
                }
                yield return new WaitForEndOfFrame();
            } 
            else
            {
                break;
            }
        }

        StartCoroutine(CountDown());
    }
}
