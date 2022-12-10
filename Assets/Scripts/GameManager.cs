using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // square distance of the closest enemy
    public float closeEnemySqrDist;
    // index of the closest enemy
    int closeEnemyIndex;
    // normalized direction vector from player to the closet enemy
    public Vector3 closeEnemyDirection;
    // player
    PlayerBehavior player;
    // lists of enemies
    public List<EnemyBehavior> enemies;
    // time
    float time;
    // time UI
    public Text timeText;
    // count of killed enemies
    public int killedEnemiesCount;
    // count of killed enemies UI
    public Text killedEnemiesText;


    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // find player
        player = FindObjectOfType<PlayerBehavior>();
        // reset elapsed time
        time = 0f;
        // reset count of killed enemies
        killedEnemiesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // update elapsed time
        ElapsedTime();
    }

    public void IncreaseKilledEnemies()
    {
        // increase count
        killedEnemiesCount++;
        // update UI
        killedEnemiesText.text = killedEnemiesCount.ToString();
    }

    void ElapsedTime()
    {
        // calculate elapsed time
        time += Time.deltaTime;
        // calculate minute
        int minute = (int)Math.Truncate(time / 60);
        // calculate second
        int second = (int)time % 60;
        // minute:second
        string stringTime;
        if (minute < 10)
        {
            if (second < 10)
            {
                stringTime = "0" + minute + ":" + "0" + second;
            }
            else
            {
                stringTime = "0" + minute + ":" + second;
            }
        }
        else
        {
            if (second < 10)
            {
                stringTime = minute + ":" + "0" + second;
            }
            else
            {
                stringTime = minute + ":" + second;
            }
        }
        // update time UI
        timeText.text = stringTime;
    }

    public void FindCloseEnemy()
    {
        // enemy lists
        enemies = new List<EnemyBehavior>(FindObjectsOfType<EnemyBehavior>());
        //print(enemies.Count);
        // no enemy
        if (enemies.Count == 0)
        {
            // do nothing
            //print("No enemy");
        }
        // more than 1 enemy
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                // calculate sqr. distance
                float distSqr = Vector3.SqrMagnitude(enemies[i].transform.position - player.transform.position);
                // first index of lists
                if (i == 0)
                {
                    closeEnemySqrDist = distSqr;
                    closeEnemyIndex = i;
                }
                // except first index
                else
                {
                    // find the closest index in lists
                    if (distSqr < closeEnemySqrDist)
                    {
                        closeEnemySqrDist = distSqr;
                        closeEnemyIndex = i;
                    }
                }
            }
            // vector toward the closest enemy
            closeEnemyDirection = (enemies[closeEnemyIndex].transform.position - player.transform.position).normalized;
        }
    }
}
