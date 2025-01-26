using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2d;
    private BubbleManager bubbleManager;
    [Header("泡泡基础属性")]
    public float directionalNumbers;//方向选择的Switch数字   1右 2左 3上 4下
    public float bubbleSpeed;    //泡泡的速度
    [Header("TimeCounter")]
    private float destroyTime = 3f;   
    private float destroyTimeCounter;
    [Header("bool")]
    private bool isShootBubble;
    private bool isSoft;
    [Header("悬浮计时器")]
    public float softTime;  //这个变量代表泡泡能停留多久
    private float softTimeCounter;
    [Header("事件监听")]
    public FloatEventSO Bubble_Speed_Event;
    
    
    
    private void Awake()
    {
        destroyTimeCounter = destroyTime;
        rb = GetComponent<Rigidbody2D>();
        circleCollider2d = GetComponent<CircleCollider2D>();
    }
    
    
    
    private void Update()
    {
        ShootBubbleTimeCounter();

    }
    
    
    
    private void FixedUpdate()
    {
        SoftIng();
        Bubble_Speed();
    }
    
    
    
    private void OnEnable()
    {
        directionalNumbers = 0;
        softTimeCounter = softTime;
        isSoft = true;
        Bubble_Speed_Event.OnFloatEventRaised += OnBubble_Speed;
    }
    
    

    private void OnDisable()
    {
        Bubble_Speed_Event.OnFloatEventRaised -= OnBubble_Speed;
    }
    
    
    private void OnBubble_Speed(float directionalNumber)
    {
        isShootBubble = true;
        directionalNumbers = directionalNumber;
    }
    
    private void SoftIng()
    {
        if(softTimeCounter > 0)
        {
            softTimeCounter -= Time.deltaTime;
        }
        if(softTimeCounter < 0 && isSoft)
        {
            isSoft = false;
            Shoot();   // 发射泡泡
        }
    }
    
    
    // 初始化方法，用于设置 BulletManager 引用
    public void Initialize(BubbleManager manager)   //依赖注入
    {
        bubbleManager = manager;
        OnBubbleLarge();
    }
    
    
    // 假设这是一个泡泡，具有一些行为
    private void OnBubbleLarge()//泡泡变大
    {
        var bubbleLarge = bubbleManager.bubbleLarge;
        transform.localScale = new Vector2(bubbleLarge, bubbleLarge);
        circleCollider2d.radius = bubbleLarge / 4;  //泡泡碰撞体的半径
    }

    private void Shoot()
    {
        // 泡泡发射的行为
        if (!isShootBubble)
        {
            isShootBubble = true;
            directionalNumbers = 3;  //测试泡泡  目前是向上飘(默认)
        }
    }
    private void ShootBubbleTimeCounter()  //泡泡返回池中的时间   应该可以用携程重写
    {
        if (isShootBubble)
        {
            destroyTimeCounter -= Time.deltaTime;
        }
        if (destroyTimeCounter <= 0)
        {
            directionalNumbers = 0;
            bubbleManager.OnBubbleReturn(this);  //用依赖注入，调用BubbleManager里面的ReturnObject
            isShootBubble = false;
            destroyTimeCounter = destroyTime;
        }
    }
    private void Bubble_Speed()
    {
        switch (directionalNumbers)
        {
            case 0:
                rb.velocity = Vector2.zero;
                break;
            case 1:
                rb.velocity = new Vector2(bubbleSpeed,0);
                break;
            case 2:
                rb.velocity = new Vector2(-bubbleSpeed, 0);
                break;
            case 3:
                rb.velocity = new Vector2(0, bubbleSpeed);
                break;
            case 4:
                rb.velocity = new Vector2(0, -bubbleSpeed);
                break;
        }
    }
    
}


