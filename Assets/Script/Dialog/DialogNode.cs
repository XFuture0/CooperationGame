using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogNode
{
    [Header("��ɫ������")]
    public string name;
    [Header("��ɫ��ͷ��")]
    public Sprite sprite;
    [TextArea, Header("�Ի�����")]
    public string content;
}