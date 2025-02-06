using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BubbleSlider : MonoBehaviour
{
    private RectTransform rectTransform;
    public RectTransform FixCanvsTransform;//锁定滑行条方向
    public Transform Player;
    [Header("事件监听")]
    public FloatEventSO BubbleSliderEvent;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (rectTransform.localScale.x >= 1)
        {
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
        FixDirction();
    }
    private void OnEnable()
    {
        rectTransform.localScale = new Vector3(0, 1, 1);
        BubbleSliderEvent.OnFloatEventRaised += OnAddSlidering;
    }

    private void OnAddSlidering(float bubbleLarge)
    {
        var Scale_X = bubbleLarge / 2;
        rectTransform.localScale = new Vector3(Scale_X, 1, 1);
    }
    private void FixDirction()
    {
        if(Player.localScale.x == 1)
        {
            FixCanvsTransform.localScale = new Vector3(1, 1, 1);
        }
        if(Player.localScale.x == -1)
        {
            FixCanvsTransform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnDisable()
    {
        rectTransform.localScale = new Vector3(0, 1, 1);
        BubbleSliderEvent.OnFloatEventRaised -= OnAddSlidering;
    }
}
