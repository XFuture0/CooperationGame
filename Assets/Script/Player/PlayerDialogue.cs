using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public static PlayerDialogue instance;
    [NonSerialized]
    public Dialogue dialogue;
    [Header("¶Ô»°¿ò")]
    public GameObject dialogueBox;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void ShowDialogue()
    {
       
        dialogueBox.SetActive(true);
        Debug.Log("Show Dialogue");

        
    }
    
}
