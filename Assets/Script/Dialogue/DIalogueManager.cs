using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class DIalogueManager : MonoBehaviour
{
    public GameObject PanelOwn;
    private PlayerInputControl inputControl;
    public Text Name;
    public Text MainText;
    private bool canSkip;
    private bool canPress;
    [Header("�ı�����")]
    public TextAsset Textin;
    private int index;
    private int TextLength;
    List<string> Textlist = new List<string>();
    private void Awake()
    {
        StartList(Textin);//�ı���ʼ���������и�
    }
    private void OnEnable()
    {
        inputControl = GetComponent<PlayerInputControl>();
        inputControl.Enable();
        inputControl.GamePlay.Skip.started += OnEnterText;
        SwitchName();
        MainText.text = Textlist[1].ToString();
        index = 2;
    }
    private void OnEnterText(InputAction.CallbackContext context)
    {
        if (index <= TextLength)
        {
            if (index == TextLength)
            {
                PanelOwn.SetActive(false);
            }
            if (!canPress && !canSkip)
            {
                StartCoroutine(TextPrint());
            }
            else if (canPress && !canSkip)
            {
                canSkip = true;
            }
        }
    }
    private IEnumerator TextPrint()
    {
        canPress = true;
        SwitchName();
        MainText.text = " ";
        for (int i = 0; i < Textlist[index].Length; i++)
        {
            if (canSkip)
            {
                MainText.text = Textlist[index];
                index++;
                canSkip = false;
                canPress = false;
                break;
            }
            MainText.text += Textlist[index][i];
            yield return new WaitForSeconds(0.1f);
            if (i == Textlist[index].Length - 1)
            {
                canPress = false;
                canSkip = false;
                index++;
            }
        }
    }
    public void StartList(TextAsset textin)//���浽�б���
    {
        Textlist.Clear();//����б�
        index = 0;
        var text = textin.text.Split('\n');
        foreach (var Text in text)
        {
            Textlist.Add(Text);
        }
        TextLength = Textlist.Count - 1;
    }
    public void SwitchName()//�л�˵��������
    {
        switch (Textlist[index])
        {
            case "A\r":
                Name.text = "Player";
                index++;
                break;
            case "B\r":
                Name.text = "???";
                index++;
                break;
        }
    }
}

