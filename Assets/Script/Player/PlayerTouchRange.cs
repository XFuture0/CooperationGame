using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRange : MonoBehaviour
{
    [Header("�㲥")]
    public VoidEventSO CallBubbleFlyEvent;//��⵱�°���->ȷ�����ݷ���
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Bubble")
        {
            CallBubbleFlyEvent.RaisedVoidEvent();
        }
    }
}
