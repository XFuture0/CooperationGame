using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] private bool canInteract = false;    
    private IInteractable targetItem;
    public GameObject signSprite;
    public Transform playerTrans;
    
    private PlayerInputControl inputControl;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.interact.started += OnConfirm;
    }



    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canInteract;
        signSprite.transform.localScale = playerTrans.localScale;
        transform.localScale = playerTrans.localScale;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canInteract)
        {
            targetItem.TriggerAction();
            //展示对话框，开始对话
            Debug.Log("Interact");
            PlayerDialogue.instance.ShowDialogue();
        }
        
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactive Object"))
        {
            canInteract = true;
            targetItem = other.GetComponent<IInteractable>();
            //接触可互动物体将对话内容传递过来
            PlayerDialogue.instance.dialogue = other.GetComponent<NPC>().dialogue;
        }

        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
    }
}
