using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [Header("Bubble Shoot")]
    public Bubble bubblePrefab;   //这里的类型为bubble 不是GameObject  想要拖进去的话必须确保prefab有Bubble脚本  
    private ObjectPool<Bubble> bubblePool;   //泛型，类型为Bubble   之后想做更多东西可以仿照泛型写 <>里面填写不同的脚本名称即可使用对象池

    void Start()
    {
        bubblePool = new ObjectPool<Bubble>(bubblePrefab);    
        
    }

    private void Update()
    {
        
    }

    public void FireBubble()
    {
        Bubble bubble = bubblePool.GetObject();  // 从池中获取泡泡
        bubble.transform.position = transform.position;  // 设置泡泡位置，作为初始位置  //TODO:改为主角身前
        bubble.Shoot();  // 发射泡泡
        
        bubble.Initialize(this);  // 调用 Initialize 方法，将 GameManager 注入到 Bubble
    }

    public void OnBubbleReturn(Bubble bubble)
    {
        bubblePool.ReturnObject(bubble);   //返回池中
    }
}

