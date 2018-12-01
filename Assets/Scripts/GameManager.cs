using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum State { SPAWNING, WAITING, PAUSE }
    public State state;

    public GameObject waveText;
    public GameObject fallingHoboSpawner;
    public GameObject fallingHobo;
    public GameObject hobo;
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
        Instantiate(fallingHobo, new Vector3(Random.Range(-48.5f, -31.5f), fallingHoboSpawner.transform.position.y, 0.0f), Quaternion.identity);
    }

    IEnumerator CountDown()
    {
        waveText.GetComponent<WaveIndicator>().WaveUpdate(wave);
        float countDownTimer = 3f;
        while(countDownTimer > 0)
        {
            countDownTimer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        state = State.SPAWNING;
        StartCoroutine(RunGame());
    }

    IEnumerator RunGame()
    {
        while(state != State.PAUSE)
        {
            if(state == State.SPAWNING)
            {
                hoboSpawners[spawnIndex1].GetComponent<Spawn>().SpawnEnemy(hobo);
                hoboSpawners[spawnIndex2].GetComponent<Spawn>().SpawnEnemy(hobo);
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
        }

        StartCoroutine(CountDown());
    }
}
