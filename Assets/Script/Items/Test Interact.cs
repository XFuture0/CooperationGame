<<<<<<< HEAD
=======
using System;
>>>>>>> e12ff6d5d79513329743d5461f828ddd6b4d0c23
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour,IInteractable
{
<<<<<<< HEAD
    private bool isInteracted = false;    //这个变量的使用仅限于单次触发后无法再触发的物体
=======
    
   
    private bool isInteracted = false;    //这个变量的使用仅限于单次触发后无法再触发的物体

  
   
>>>>>>> e12ff6d5d79513329743d5461f828ddd6b4d0c23
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
