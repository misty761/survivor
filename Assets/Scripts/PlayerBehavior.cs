using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // move speed
    public float speed = 1f;
    // max. life
    public int healthMax = 100;
    // current life
    int health;
    // fire interval
    public float fireInterval = 1f;
    // time after firing
    float fireTime;
    // firing squre distance
    public float fireSqrDist = 100f;
    // bullet prefab
    public GameObject bullet;
    // exp. current
    int exp;
    // exp. next
    int expNext;
    // player level
    int level;
    // hp bar
    public Image hpBar;
    // exp bar
    public Image expBar;
    // text level
    public Text textLevel;
    // joystick
    Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        hpBar.fillAmount = (float)health / (float)healthMax;
        fireTime = fireInterval;
        exp= 0;
        expNext = 10;
        expBar.fillAmount = (float)exp / (float)expNext;
        level = 1;
        textLevel.text = "lv." + level;
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Attack();
    }

    private void Move()
    {
        #region keyboard
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

        #region joystick
        // joystick
        float h = joystick.Horizontal;
        float v = joystick.Vertical;
        float joystickMin = 0.1f;
        // sprite
        if (h > 0f)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if (h < 0f)
        {
            transform.localScale = new Vector3(-1, 1);
        }
        // move
        if (h > joystickMin || h <-joystickMin)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * h);
        }
        if (v > joystickMin || v <-joystickMin) 
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * v);
        }
        #endregion
    }

    void Attack()
    {
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
            print("player is damaged!");
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
        health -= damage;
        hpBar.fillAmount = (float)health / (float)healthMax;
        //print("Hp : " + health);
        if (health <= 0)
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
        exp += point;
        // update exp. bar
        expBar.fillAmount = (float)exp / (float)expNext;
        //print(expCurrent);
        // check level up
        if (exp >= expNext)
        {
            // increase exp. thereshold for the next level
            expNext = expNext * 2;
            // reset current exp.
            exp = 0;
            // update exp. bar
            expBar.fillAmount = (float)exp / (float)expNext;
            // level up
            level++;
            // update level UI
            textLevel.text = "lv." + level;
        }
    }
}
