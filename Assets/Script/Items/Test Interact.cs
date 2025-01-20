using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour,IInteractable
{
    private bool isInteracted = false;    //这个变量的使用仅限于单次触发后无法再触发的物体
    public void TriggerAction()
    {
        if (!isInteracted)
        {
            TriggerTestInteract();
        }
    }

    private void TriggerTestInteract()
    {
        Debug.Log("TriggerAction");
        isInteracted = true;
        gameObject.tag = "Untagged";
    }
}
