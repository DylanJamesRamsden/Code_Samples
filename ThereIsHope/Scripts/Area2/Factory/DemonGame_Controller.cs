using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonGame_Controller : MonoBehaviour
{
    public Area2_Manager a2manager;

    [Header ("Game Variables")]
    public int noOfDemons2spawn;
    int demonSpawnCounter;
    public int demonsAlive;
    int demonSpawnTimer;

    int wave = 1;
    public int waveResetTime;
    int waveResetCounter;

    public bool WaveDone = false;

    public GameObject[] demonSpawnPoints;
    public GameObject[] Demons;

    [Header ("UI Variables")]
    public Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        winCondition();

        if (a2manager.inDemonGame == true)
        {
            if (WaveDone == false)
            {
                spawnDemons();
            }
            else
            {
                if (waveResetCounter < waveResetTime)
                {
                    waveResetCounter++;
                }
                else
                {
                    noOfDemons2spawn += 3;
                    wave++;
                    WaveDone = false;

                    Debug.Log("Wave Finished" + wave);
                }
            }
        }

    }

    void spawnDemons()
    {
        if (demonSpawnCounter < noOfDemons2spawn)
        {
            if (demonSpawnTimer < 60)
            {
                demonSpawnTimer++;
            }
            else
            {
                int chosenSpawn = Random.Range(0, 9);
                int chosenDemon = Random.Range(0, 100);

                if (chosenDemon < 60)
                {
                    Instantiate(Demons[0], demonSpawnPoints[chosenSpawn].transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Demons[1], demonSpawnPoints[chosenSpawn].transform.position, Quaternion.identity);
                }

                demonSpawnTimer = 0;
                demonSpawnCounter++;
                demonsAlive++;
            }
        }
        else
        {
            enemiesLeft();
        }
    }

    void enemiesLeft()
    {
        if (demonsAlive == 0)
        {
            WaveDone = true;
        }
    }

    void winCondition()
    {
        if (wave == 4 && demonsAlive == 0)
        {
            a2manager.demonGameFinished = true;
            a2manager.inDemonGame = false;
        }
    }
}
