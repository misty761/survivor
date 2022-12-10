using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
	public float moveSpeed = 1f;
	public float destroyTime = 1f;
	float elapsedTime;
	Vector3 pos;

    private void Start()
    {
		elapsedTime = destroyTime;
    }

    void Update()
	{
		// move up
		pos.Set(transform.position.x, transform.position.y
			+ (moveSpeed + Time.deltaTime), transform.position.z);
		transform.position = pos;

		// 점점 작아짐
		float scale = Mathf.Lerp(elapsedTime, destroyTime, 0.8f);
		transform.localScale = new Vector3(scale, scale);

		// elapsed time
		elapsedTime -= Time.deltaTime;
        // destroy after destroyTime
        if (elapsedTime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
