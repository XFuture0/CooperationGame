using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogNode
{
    [Header("角色的名字")]
    public string name;
    [Header("角色的头像")]
    public Sprite sprite;
    [TextArea, Header("对话内容")]
    public string content;
}