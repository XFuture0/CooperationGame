using UnityEngine;

[CreateAssetMenu(fileName = "创建对话", menuName = "对话")]
public class Dialogue : ScriptableObject
{
    //对话节点
    public DialogNode[] nodes;
}
