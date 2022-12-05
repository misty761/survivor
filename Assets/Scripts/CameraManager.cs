﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //static public CameraManager instance;

    // 박스 컬라이더 영역의 최소 초대값
    BoxCollider2D bound;

    Vector3 minBound;
    Vector3 maxBound;

    PlayerBehavior target;            // 카메라가 따라갈 대상

    Vector3 targetPosition;             // 대상의 현재 위치

    // 카메라의 반넓이와 반높이 값 변수
    float halfWidth;
    float halfHeight;
    float moveSpeed = 100f;             // 카메라가 따라갈 속도

    // 반 높이를 구하기 위해 필요한 카메라 변수
    Camera theCamera;

    private void Awake()
    {
        /*
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerBehavior>();
        theCamera = GetComponent<Camera>();
        bound = GetComponentInChildren<BoxCollider2D>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
