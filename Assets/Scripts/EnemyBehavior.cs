using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // move speed
    public float speed = 3f;
    // player
    PlayerBehavior player;
    // power of enemy(damage to player)
    public int power = 1;
    // max. life
    public int lifeMax = 100;
    // current life
    public int lifeCurrent;
    // exp
    public GameObject prefabExp;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
        lifeCurrent = lifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        #region move
        // vector toward player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        //print(direction);
        // move oward player
        transform.Translate(direction * Time.deltaTime * speed);
        // rotate sprite
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1);
        }
        #endregion
    }

    public void Damaged(int damage)
    {
        // reduce life
        lifeCurrent -= damage;
        if (lifeCurrent <= 0f)
        {
            // die
            Die();
        }
    }

    void Die()
    {
        // instantiate exp
        GameObject goEXp = Instantiate(prefabExp, transform.position, transform.rotation);
        ExpMarble exp = goEXp.GetComponent<ExpMarble>();
        exp.expPoint = power * lifeMax / 100;
        // destroy enemy
        Destroy(gameObject);
    }
}