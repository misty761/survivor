using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // speed of bullet
    public float speed = 4f;
    // direction toward the closest enemy
    Vector3 directionMove;
    // damage to enemy
    public int power = 100;

    // Start is called before the first frame update
    void Start()
    {
        // find the colosest enemy
        GameManager.instance.FindCloseEnemy();
        // count of enemies
        int countEnemy = GameManager.instance.enemies.Count;
        // no enemy
        if (countEnemy == 0)
        {
            // destroy bullet
            Destroy(gameObject);
        }
        // enemy is exist
        else
        {
            // normalized vector to the closet enemy
            directionMove = GameManager.instance.closeEnemyDirection;
            //print(directionMove);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // move
        // move toward the closest enemy
        transform.Translate(directionMove * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //print(other);
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            // attack enemy
            enemy.Damaged(power);
            // destroy bullet
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall for Bullet"))
        {
            //print(other);
            // destroy bullet which is out of camera
            Destroy(gameObject);
        }
    }
}
