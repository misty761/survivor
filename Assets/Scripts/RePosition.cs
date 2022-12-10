using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 posPlayer = collision.transform.position;
            Vector3 posThis = transform.position;
            float diffX = posPlayer.x - posThis.x;
            float diffXAbs = Mathf.Abs(diffX);
            float diffY = posPlayer.y - posThis.y;
            float diffYAbs = Mathf.Abs(diffY);
            if (diffXAbs > diffYAbs) 
            {
                if (diffX > 0)
                {
                    transform.Translate(Vector3.right * 80);
                }
                else
                {
                    transform.Translate(Vector3.left * 80);
                }
            }
            else
            {
                if (diffY > 0)
                {
                    transform.Translate(Vector3.up * 80);
                }
                else
                {
                    transform.Translate(Vector3.down * 80);
                }
            }  
        }
    }
}
