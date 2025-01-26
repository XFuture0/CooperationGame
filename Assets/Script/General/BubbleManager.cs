using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleManager : MonoBehaviour
{
    [Header("Bubble Shoot")]
    public Bubble bubblePrefab;   //这里的类型为bubble 不是GameObject  想要拖进去的话必须确保prefab有Bubble脚本  
    private ObjectPool<Bubble> bubblePool;   //泛型，类型为Bubble   之后想做更多东西可以仿照泛型写 <>里面填写不同的脚本名称即可使用对象池
    private Vector3 bubblePosition;
    
    
    public float offsetX;//泡泡生成在玩家面前的偏移值
    [HideInInspector]public float bubbleLarge;//临时储存泡泡的大小
    
    
    [Header("事件监听")]
    public TransFormEventSO Transform_Bubble_To_PlayerEvent;
    public FloatEventSO BubbleMaxEvent;
    void Start()
    {
        bubblePool = new ObjectPool<Bubble>(bubblePrefab);    
        
    }

    private void Update()
    {
        
    }
    private void OnEnable()
    {
        Transform_Bubble_To_PlayerEvent.OnTransformEventRaised += OnGetBubblePo;
        BubbleMaxEvent.OnFloatEventRaised += OnGetBubbleLarge;
    }

    private void OnGetBubbleLarge(float large)
    {
        bubbleLarge = large;
    }

    private void OnGetBubblePo(Transform player)
    {
        if(player.localScale.x == 1)
        {
            bubblePosition = new Vector3(player.position.x - offsetX, player.position.y, player.position.z);
        }
        if(player.localScale.x == -1)
        {
            bubblePosition = new Vector3(player.position.x + offsetX, player.position.y, player.position.z);
        }
    }

    private void OnDisable()
    {
        Transform_Bubble_To_PlayerEvent.OnTransformEventRaised -= OnGetBubblePo;
        BubbleMaxEvent.OnFloatEventRaised -= OnGetBubbleLarge;
    }

    public void FireBubble()
    {
        Bubble bubble = bubblePool.GetObject();  // 从池中获取泡泡
        bubble.transform.position = bubblePosition;  // 设置泡泡位置，作为初始位置  
        bubble.Initialize(this);  // 调用 Initialize 方法，将 GameManager 注入到 Bubble
    }

    public void OnBubbleReturn(Bubble bubble)
    {
        bubblePool.ReturnObject(bubble);   //返回池中
    }
}

