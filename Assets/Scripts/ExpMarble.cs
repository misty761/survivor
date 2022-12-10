using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpMarble : MonoBehaviour
{
    // exp. point from enemy
    public int expPoint;
    public float scaleMax = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        // 경험치 양에 따라 크기수정
        float scale = expPoint / 1f;
        if (scale > scaleMax )
        {
            scale = scaleMax;
        }
        transform.localScale = new Vector3(scale, scale);
    }
}
