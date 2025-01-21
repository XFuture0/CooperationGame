using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Bubble Shoot")]
    public Bubble bubblePrefab;
    private ObjectPool<Bubble> bubblePool;

    void Start()
    {
        bubblePool = new ObjectPool<Bubble>(bubblePrefab);
        
    }
    
    public void FireBubble()
    {
        Bubble bubble = bubblePool.GetObject();  // 从池中获取泡泡
        bubble.transform.position = transform.position;  // 设置泡泡位置
        bubble.Shoot(Vector3.forward);  // 发射泡泡
    }

    void OnBubbleReturn(Bubble bubble)
    {
        bubblePool.ReturnObject(bubble);
    }
}

