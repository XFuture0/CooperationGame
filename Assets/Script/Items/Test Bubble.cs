using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("TimeCounter")]
    private float destroyTime = 3f;
    private float destroyTimeCounter;
    [Header("bool")]
    private bool isShootBubble;
    
    private BubbleManager bubbleManager;

    private void Awake()
    {
        destroyTimeCounter = destroyTime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ShootBubbleTimeCounter();

    }
    
    

    // 初始化方法，用于设置 BulletManager 引用
    public void Initialize(BubbleManager manager)   //依赖注入
    {
        bubbleManager = manager;
    }

    // 假设这是一个泡泡，具有一些行为
    public void Shoot()
    {
        // 泡泡发射的行为
        
        isShootBubble = true;   
        rb.AddForce(transform.up * 10f, ForceMode2D.Impulse);  //测试泡泡  目前是向上飘
        
    }

    private void ShootBubbleTimeCounter()  //泡泡返回池中的时间   应该可以用携程重写
    {
        if (isShootBubble)
        {
            destroyTimeCounter -= Time.deltaTime;
        }

        if (destroyTimeCounter <= 0)
        {
            bubbleManager.OnBubbleReturn(this);  //用依赖注入，调用BubbleManager里面的ReturnObject
            isShootBubble = false;
            destroyTimeCounter = destroyTime;
        }
    }

    
}


