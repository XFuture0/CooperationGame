using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private BubbleManager bubbleManager;
    private Vector2 inputDirection;
    public GameObject touchRange;
    public GameObject BubbleSlider;//泡泡变大的进度条(UI)
    [Header("泡泡属性")]
    private float bubbleLarge;//泡泡的大小
    public float bubbleLargeIng;//泡泡的变大速率
    public float bubbleMax;//泡泡最大大小
    public float bubbleMin;//泡泡最小大小
    [Header("基础属性")]
    public float moveSpeed;
    public float jumpForce;
    [Header("bool")] 
    [SerializeField]private bool isBubble;
    [Header("广播")]
    public TransFormEventSO Transform_Bubble_To_PlayerEvent;//释放泡泡时将泡泡生成在玩家面前
    public FloatEventSO BubbleMaxEvent;//传递泡泡的大小的数值
    public FloatEventSO BubbleSliderEvent;//传递泡泡进度条的数值
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        bubbleManager = FindObjectOfType<BubbleManager>();
        
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Touch.started += SetBubbleRange;//产生泡泡的感应环
        inputControl.GamePlay.Bubble.performed += BubbleStart;
        inputControl.GamePlay.Bubble.canceled += BubbleEnd;

    }
    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Start()
    {
        bubbleLarge = 0;
    }

    private void Update()
    {
        BubbleSliderEvent.RaisedFloatEvent(bubbleLarge);
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        BlowBubbles();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    

    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);
        var faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = -1;
        if (inputDirection.x < 0)
            faceDir = 1;
        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    }
    
    
    
    
    //TODO:跳跃优化
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
    
    
    //TODO:思路1（长按让泡泡变大）  
    //下面这个就是吹泡泡，按下开始吹，松开就结束，可以设计泡泡爆炸或者什么
    private void BubbleStart(InputAction.CallbackContext obj)
    {
        BubbleSlider.SetActive(true);
        Debug.Log("BubbleStart");
        isBubble = true;
    }
    
    
    private void BubbleEnd(InputAction.CallbackContext obj)
    {
        Debug.Log("BubbleEnd");
        BubbleSlider.SetActive(false);
        isBubble = false;
        if(bubbleLarge < bubbleMin)
        {
            bubbleLarge = bubbleMin;//泡泡最小大小
        }
        Transform_Bubble_To_PlayerEvent.RaisedTransFormEvent(this.gameObject.transform);//传递玩家坐标
        BubbleMaxEvent.RaisedFloatEvent(bubbleLarge);
        bubbleManager.FireBubble();
        bubbleLarge = 0;
    }

    private void BlowBubbles()
    {
        if (isBubble)
        {
            if (bubbleLarge < bubbleMax)
            {
                bubbleLarge += bubbleLargeIng;
            }
            else
            {
                bubbleLarge = bubbleMax;//泡泡最大大小
            }
        }
        //调用gameManager里面发射泡泡的函数

    }
    
    
    
    private void SetBubbleRange(InputAction.CallbackContext context)
    {
        touchRange.SetActive(true);
        StartCoroutine(CloseTouchRange());
    }
    
    
    
    private IEnumerator CloseTouchRange()//用携程关闭感应环
    {
        yield return new WaitForSeconds(0.5f);
        touchRange.SetActive(false);
    }
}
