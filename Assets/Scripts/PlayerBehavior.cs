using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // move speed
    public float speed = 1f;
    // max. life
    public int lifeMax = 100;
    // current life
    int lifeCurrent;
    // fire interval
    public float fireInterval = 1f;
    // time after firing
    float fireTime;
    // firing squre distance
    public float fireSqrDist = 100f;
    // bullet prefab
    public GameObject bullet;
    // exp. current
    int expCurrent;
    // exp. next
    int expNext;
    // player level
    int level;

    // Start is called before the first frame update
    void Start()
    {
        lifeCurrent = lifeMax;
        fireTime = fireInterval;
        expCurrent= 0;
        expNext = 10;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        #region move
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                // do nothing
            }
            else
            {
                // move left
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                // rotate sprite
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                // do nothing
            }
            else
            {
                // move right
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                // rotate sprite
                transform.localScale = new Vector2(1, 1);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.S))
            {
                // do nothing
            }
            else
            {
                // move up
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                // do nothing
            }
            else
            {
                // move down
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        #endregion

        #region attack
        // calculate time after firing
        fireTime += Time.deltaTime;
        // fire after fire interval
        if (fireTime >= fireInterval)
        {
            // find a close enemy
            GameManager.instance.FindCloseEnemy();
            //print(GameManager.instance.closeEnemySqrDist);
            // count of enemies
            int countEnemy = GameManager.instance.enemies.Count;
            // squre distance of the closest enemy
            float closeEnemyDist = GameManager.instance.closeEnemySqrDist;
            // if enemy is exist
            if (countEnemy > 0)
            {
                // 적이 fireSqrDist보다 가까울 때
                if (closeEnemyDist < fireSqrDist)
                {
                    // reset fire time
                    fireTime = 0f;
                    // fire
                    Instantiate(bullet, transform.position, transform.rotation);
                } 
            }       
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision);
        // collision to enemy
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //print(collision);
            // attacked by enemy
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            // player is damaged
            Damaged(enemy.power);
        }
        // collision to Exp
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Exp"))
        {
            ExpMarble exp = collision.gameObject.GetComponent<ExpMarble>();
            // Exp ++
            IncreaseExp(exp.expPoint);
            // destroy Exp
            Destroy(collision.gameObject);
        }
    }

    void Damaged(int damage)
    {
        // lost life
        lifeCurrent -= damage;
        print(lifeCurrent);
        if (lifeCurrent <= 0f)
        {
            // player die
            Die();
        }
    }

    void Die()
    {

    }

    void IncreaseExp(int point)
    {
        // get exp
        expCurrent += point;
        //print(expCurrent);
        // check level up
        if (expCurrent > expNext)
        {
            // increase exp. thereshold for the next level
            expNext = expNext * 2;
            // reset current exp.
            expCurrent = 0;
            // level up
            level++;
            print(expNext);
            print(level);
        }

    }
}
