using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum State { SPAWNING, WAITING, PAUSE, STOP}
    public State state;
    public TextMeshProUGUI tpro;

    public GameObject waveText;
    public GameObject gameOverPanel;
    public GameObject fallingHoboSpawner;
    public GameObject fallingHobo;
    public List<GameObject> hobos = new List<GameObject>();
    public GameObject[] hoboTypes;
    public List<GameObject> hoboSpawners = new List<GameObject>();
    public GameObject[] hoboSpawnLoc;
    public int enemiesKilled = 0;
    public int wave = 0;
    public int hoboIndex = 0;

    public int numToKill;
    private int numSpawning;
    public int baseNumSpawning;
    private int score;

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

    public int ScoreUpdate
    {
        set { score = value; }
        get { return score; }
    }


    private void Start()
    {
        state = State.PAUSE;
        resetTimer = timer;
        SelectWave();
    }

    private void Update()
    {
        tpro.text = score.ToString();
    }

    private void SelectWave()
    {
        wave += 1;
        PickSpawnLocations();
    }

    private void PickSpawnLocations()
    {
        // pick a random number of spawn locations
        int numOfSpawnLocations = Random.Range(2, 5);
        // clear list if it's not empty
        if(hoboSpawners.Count > 0)
        {
            hoboSpawners.Clear();
        }
        // pick random spawn locations out the 4 available and add it to a list
        for(int i = 0; i < numOfSpawnLocations; i++)
        {
            hoboSpawners.Add(hoboSpawnLoc[Random.Range(0, 4)]);
        }

        AssembleWaves();
    }

    private void AssembleWaves()
    {
        hoboIndex = 0;
        // set a number of hobos to spawn
        numSpawning = baseNumSpawning * wave / 2;
        numToKill = numSpawning;
        // clear list if it's not empty
        if (hobos.Count > 0)
        {
            hobos.Clear();
        }
        // create list of hobos to spawn
        for (int i = 0; i < numSpawning; i++)
        {
            
            int maxChances = GetMaxChances();
            int chances = Random.Range(1, maxChances + 1);

            if (wave < 2)
            {
                hobos.Add(hoboTypes[0]);
            }
            else if (chances <= 5)
            {
                hobos.Add(hoboTypes[1]);
            }
            else if (chances > 5 && chances <= 10)
            {
                hobos.Add(hoboTypes[2]);
            }
            else
            {
                hobos.Add(hoboTypes[0]);
            }
        }

        StartCoroutine(CountDown());
    }

    private int GetMaxChances()
    {
        int maxChances = 100;
        if (wave < 25)
        {
            maxChances = 101 - (wave * 2);
        }
        else
        {
            maxChances = 50;
        }

        return maxChances;
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
        while (state != State.PAUSE)
        {
            if(state == State.SPAWNING)
            {
                // pick one of the spawn locations from the list
                int spawnIndex = Random.Range(0, hoboSpawners.Count);
                // send it the information to spawn a hobo
                hoboSpawnLoc[spawnIndex].GetComponent<Spawn>().SpawnEnemy(hobos[hoboIndex]);
                // tell the game that it spawned a hobo
                numSpawning -= 1;
                hoboIndex += 1;

                if (numSpawning <= 0)
                {
                    state = State.WAITING;
                }

                yield return new WaitForSeconds(.5f);
            }
            else if (state == State.WAITING)
            {
                if (numToKill <= 0 && numSpawning <= 0)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        timer = resetTimer;
                        SelectWave();
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



    private void PlayerDead()
    {
        state = State.STOP;
        if(enemiesKilled == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void MeatGrinder()
    {
        Instantiate(fallingHobo, new Vector3(Random.Range(-41f, -39f), fallingHoboSpawner.transform.position.y, -1f), Quaternion.identity);
    }
}
