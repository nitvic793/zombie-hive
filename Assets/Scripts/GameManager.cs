using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool isGameOver = false;
    Text gameOver;
    Text escaped;
    Text killed;
    public int SoldiersKilled = 0;
    public int SoldiersEscaped = 0;

    // Use this for initialization
    void Start()
    {
        gameOver = GameObject.Find("Text").GetComponent<Text>();
        escaped = GameObject.Find("Escaped").GetComponent<Text>();
        killed = GameObject.Find("Killed").GetComponent<Text>();
        gameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        escaped.text = "Soldiers Killed: " + SoldiersKilled;
        killed.text = "Soldiers Escaped: " + SoldiersEscaped;
        var zombies = GameObject.FindGameObjectsWithTag("Zombie");
        int count = 0;
        foreach (var zombie in zombies)
        {
            if (zombie.GetComponent<FollowLeader>().Health != 0F) count++;
        }

        if (count == 0)
        {
            GameObject.Find("ZombieSound").GetComponent<AudioSource>().Stop();
            isGameOver = true;
            gameOver.text = "Game Over!";
        }

        GameObject.FindGameObjectsWithTag("Soldier");

        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            gameOver.text = gameOver.text == "Paused" ? "" : "Paused";
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
