using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
   private Dialogue dialogue;
   private int index;
   public TextMeshProUGUI text;
   public TextMeshProUGUI nameText;
   public SpriteRenderer icon;
   private PlayerInputControl inputControl;
    private void Awake()
    {
        
        inputControl=new PlayerInputControl();
        inputControl.GamePlay.interact.started += Continue;
    }

    private void Continue(InputAction.CallbackContext context)
    {
        if (dialogue != null && index < dialogue.nodes.Length)
        {
            ShowDialogue();
        }else if(dialogue!=null && index >= dialogue.nodes.Length)
        {
            index = 0;
            gameObject.SetActive(false);
            Debug.Log("End of Dialogue");
        }
    }

    private void OnEnable()
    {
        inputControl.Enable();
        dialogue = PlayerDialogue.instance.dialogue;
        index = 0;
        ShowDialogue();
    }
    private void OnDisable()
    {
        //inputControl.Disable();
    }
    private void ShowDialogue()
    {
        //获取当前节点，并且更新下一个节点
        DialogNode node = dialogue.nodes[index++];
        //显示对话内容
        text.text = node.content;
        //显示角色头像
        icon.sprite = node.sprite;
        //显示角色名字
        nameText.text = node.name;
    }
    
}
