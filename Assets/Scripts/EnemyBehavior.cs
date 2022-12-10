using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    // move speed
    public float speed = 3f;
    // player
    PlayerBehavior player;
    // power of enemy(damage to player)
    public int power = 1;
    // max. life
    public int healthMax = 100;
    // current life
    public int health;
    // prefab of exp.
    public GameObject prefabExp;
    // prefab of damage text
    public GameObject prefabDamageText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
        health = healthMax;
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
        health -= damage;
        // text damage
        RectTransform damageText = Instantiate(prefabDamageText, transform.position, transform.rotation, GameObject.Find("Canvas").transform).GetComponent<RectTransform>();
        Vector3 _pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, 0));
        damageText.transform.position = _pos;
        damageText.gameObject.transform.GetComponent<Text>().text = "" + damage;
        // check die
        if (health <= 0f)
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
        // calculate exp. point
        exp.expPoint = power * healthMax / 100;
        // increase count of killed enemies
        GameManager.instance.IncreaseKilledEnemies();

        // destroy enemy
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어와 충돌시 멀리 팅겨나가지 않도록
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
        }
    }
}