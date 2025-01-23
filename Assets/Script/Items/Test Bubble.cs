using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2d;
    public float SpeedChoose;//方向选择
    public float Speed;//泡泡的速度
    [Header("TimeCounter")]
    private float destroyTime = 3f;
    private float destroyTimeCounter;
    [Header("bool")]
    private bool isShootBubble;
    private bool isSoft;
    private BubbleManager bubbleManager;
    [Header("悬浮计时器")]
    public float Soft_Time;
    private float Soft_Time_Count;
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
        SpeedChoose = 0;
        Soft_Time_Count = Soft_Time;
        isSoft = true;
        Bubble_Speed_Event.OnFloatEventRaised += OnBubble_Speed;
    }

    private void OnBubble_Speed(float speedChoose)
    {
        isShootBubble = true;
        SpeedChoose = speedChoose;
    }

    private void OnDisable()
    {
        Bubble_Speed_Event.OnFloatEventRaised -= OnBubble_Speed;
    }
    private void SoftIng()
    {
        if(Soft_Time_Count > 0)
        {
            Soft_Time_Count -= Time.deltaTime;
        }
        if(Soft_Time_Count < 0 && isSoft)
        {
            isSoft = false;
            Shoot();// 发射泡泡
        }
    }
    // 初始化方法，用于设置 BulletManager 引用
    public void Initialize(BubbleManager manager)   //依赖注入
    {
        bubbleManager = manager;
        OnBubbleLarge();
    }
    // 假设这是一个泡泡，具有一些行为
    public void OnBubbleLarge()//泡泡变大
    {
        var Bubblelarge = bubbleManager.BubbleLarge;
        transform.localScale = new Vector3(Bubblelarge, Bubblelarge, transform.localScale.z);
        circleCollider2d.radius = Bubblelarge / 4;//泡泡碰撞体的半径
    }
    public void Shoot()
    {
        // 泡泡发射的行为
        if (!isShootBubble)
        {
            isShootBubble = true;
            SpeedChoose = 3;  //测试泡泡  目前是向上飘(默认)
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
            SpeedChoose = 0;
            bubbleManager.OnBubbleReturn(this);  //用依赖注入，调用BubbleManager里面的ReturnObject
            isShootBubble = false;
            destroyTimeCounter = destroyTime;
        }
    }
    private void Bubble_Speed()
    {
        switch (SpeedChoose)
        {
            case 0:
                rb.velocity = Vector2.zero;
                break;
            case 1:
                rb.velocity = new Vector2(Speed,0);
                break;
            case 2:
                rb.velocity = new Vector2(-Speed, 0);
                break;
            case 3:
                rb.velocity = new Vector2(0, Speed);
                break;
            case 4:
                rb.velocity = new Vector2(0, -Speed);
                break;
        }
    }
    
}


