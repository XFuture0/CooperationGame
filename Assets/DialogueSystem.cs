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
        //��ȡ��ǰ�ڵ㣬���Ҹ�����һ���ڵ�
        DialogNode node = dialogue.nodes[index++];
        //��ʾ�Ի�����
        text.text = node.content;
        //��ʾ��ɫͷ��
        icon.sprite = node.sprite;
        //��ʾ��ɫ����
        nameText.text = node.name;
    }
    
}
