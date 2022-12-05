using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
