using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRange : MonoBehaviour
{
    [Header("广播")]
    public VoidEventSO CallBubbleFlyEvent;//检测当下按键->确定泡泡方向
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Bubble")
        {
            CallBubbleFlyEvent.RaisedVoidEvent();
        }
    }
}
