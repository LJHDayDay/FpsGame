﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표: W,A,S,D키를 누르면 캐릭터를 그방향으로 이동시키고 싶다.
//필요속성: 속도, 방향
//순서1. 사용자의 입력을 받는다.
//순서2. 이동 방향을 설정한다.
//순서3. 이동 속도에 따라 나를 이동시킨다.

//목표2: 스페이스를 누르면 수직으로 점프하고 싶다.
//필요속성: 캐릭터 컨트롤러, 중력변수, 수직 속력 변수, 점프파워
//2-1. 캐릭터 수직 속도에 중력을 적용하고 싶다.
//2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
//2-3. 스페이스 키를 누르면 수직속도에 점프파워를 적용하고싶다.

//목표3. 점프 중인지 확인하고, 점프 중이면 점프 전 상태로 초기화 하고 싶다.


public class PlayerMove : MonoBehaviour
{

    //필요속성: 속도, 방향
    public float speed = 10.0f;

    //필요속성: 캐릭터 컨트롤러, 중력변수, 수직 속력 변수
    CharacterController characterController;
    float gravity = -20f;
    float yVelocity = 0;
    public float jumpPower = 10;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //순서1. 사용자의 입력을 받는다.    
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //만약 점프 중이었다면 점프 전 상태로 초기화 하고 싶다.
        if(isJumping && characterController.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;

            yVelocity = 0;
        }

        else if (characterController.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
        }

        //2-3. 스페이스 키를 누르면 수직속도에 점프파워를 적용하고싶다.
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping=true;
        }

        //순서2. 이동 방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);

        //카메라 기준으로 방향을 바꾼다.
        dir = Camera.main.transform.TransformDirection(dir);

        //2-1. 캐릭터 수직 속도에 중력을 적용하고 싶다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //순서3. 이동 속도에 따라 나를 이동시킨다.  
        //transform.position += dir * speed * Time.deltaTime;

        //2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
        characterController.Move(dir * speed *  Time.deltaTime);

        
    }
}